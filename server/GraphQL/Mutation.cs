using FluentValidation.Results;

using GPPlanetGQL.Discord;
using GPPlanetGQL.Exceptions;
using GPPlanetGQL.Models;
using GPPlanetGQL.Services;
using GPPlanetGQL.Variables;

using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Subscriptions;

using System.Security.Claims;

using static GPPlanetGQL.GraphQL.Inputs;
using static GPPlanetGQL.GraphQL.Unions;
using static GPPlanetGQL.Models.Donateitem;
using static GPPlanetGQL.Models.Reportmessage;
using static GPPlanetGQL.Models.User;

namespace GPPlanetGQL.GraphQL
{

    public class Mutation
    {
        /*
         * TODO: Remove test mutation
         */
        [Authorize]
        public bool Test(
                [Service] gpplanetContext ctx,
                [Service] ITopicEventSender sender)
        {
            return true;
        }

        [Authorize]
        public async Task<bool> SendSystemNotification(
                int? toid,
                string message,
                [Service] gpplanetContext ctx,
                [Service] LimeService lime,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal,
                [Service] ITopicEventSender sender
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException("Вашего аккаунта нет на сервере Minecraft");
            if ((user.Permissions & Permissions.All) == 0) throw new ForbiddenException("У вас нет прав!");
            if (message.Length < 1 || message.Length > 128) throw new UserInputException("Неверная длина строки. От 1 до 128");
            if (toid == null)
            {
                var user_ids = ctx.Users.Select(u => u.UserId).ToArray();
                foreach (var id in user_ids)
                {
                    var n = ctx.Systemnotifications.Add(new Systemnotification { ToId = id, Message = message });
                    ctx.SaveChanges();
                    _ = sender.SendAsync($"{id}_NewNotification", (INotification)n.Entity);
                }
            }
            else
            {
                var n = ctx.Systemnotifications.Add(new Systemnotification { ToId = (int)toid, Message = message });
                ctx.SaveChanges();
                _ = sender.SendAsync($"{toid}_NewNotification", (INotification)n.Entity);
            }
            return true;
        }

        [Authorize]
        public User AddSocialPoints(
                int id,
                int socialPoints,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal)
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            if ((user.Permissions & (Permissions.ModifySocialPoints + Permissions.All)) == 0) throw new ForbiddenException("У вас нет прав!");
            var u = ctx.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (u == null) throw new DoesNotExistsException("Пользователь не найден");
            u.SocialPoints += socialPoints;
            ctx.SaveChanges();
            return u;
        }

        [Authorize]
        public bool Login(
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) return false;
            if (user.IsBanned) throw new ForbiddenException("Вы забанены");
            return true;
        }

        [Authorize]
        public async Task<bool> InitialUsersAdd(
                [Service] gpplanetContext ctx,
                [Service] LimeService lime,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException("Вашего аккаунта нет на сервере Minecraft");
            if ((user.Permissions & Permissions.All) == 0) throw new ForbiddenException("У вас нет прав!");
            var data = await lime.GetUserDatas();

            foreach (var discord in data.Keys.ToArray())
            {
                if (discord == discordId) continue;
                var u = ctx.Users.Where(u => u.DiscordId == long.Parse(discord)).FirstOrDefault();
                if (u != null) continue;
                ctx.Users.Add(new User
                {
                    DiscordId = long.Parse(discord)
                });
            }
            ctx.SaveChanges();
            return true;
        }

        [Authorize]
        public Forum CreateForum(
                ForumCreateInput input,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            if ((user.Permissions & (Permissions.ModifyForum + Permissions.All)) == 0) throw new ForbiddenException("У вас нет прав!");
            if (input.Name.Length < 1 || input.Name.Length > 128)
            {
                throw new UserInputException("Длина названия должна быть от 1 до 128");
            }
            if (input.Link != null && input.Link.Length != 0)
            {
                if (input.Link.Length < 1 || input.Link.Length > 256)
                {
                    throw new UserInputException("Длина ссылки должна быть от 1 до 256");
                }
            }
            if (input.Link.Length == 0) input.Link = null;
            var f = ctx.Forums.Add(new Forum
            {
                Name = input.Name,
                ParentForumId = input.ParentForumId,
                Link = input.Link,
                IsOpen = input.IsOpen
            });
            ctx.SaveChanges();
            return f.Entity;
        }

        [Authorize]
        public Forum EditForum(
                ForumEditInput input,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            if ((user.Permissions & (Permissions.ModifyForum + Permissions.All)) == 0) throw new ForbiddenException("У вас нет прав!");
            var f = ctx.Forums.Where(f => f.ForumId == input.Id).FirstOrDefault();
            if (f == null) throw new DoesNotExistsException($"Форума с ID {input.Id} не существует");
            if (input.Link != null && input.Link.Length != 0)
            {
                if (input.Link.Length < 1 || input.Link.Length > 256)
                {
                    throw new UserInputException("Длина ссылки должна быть от 1 до 256");
                }
                f.Link = input.Link;
            }
            if (input.ParentForumId != null)
            {
                f.ParentForumId = input.ParentForumId;
            }
            if (input.Name != null)
            {
                if (input.Name.Length < 1 || input.Name.Length > 128)
                {
                    throw new UserInputException("Длина названия должна быть от 1 до 128");
                }
                f.Name = input.Name;
            }
            ctx.SaveChanges();
            return f;
        }

        [Authorize]
        public Forum RemoveForum(
                int id,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            if ((user.Permissions & (Permissions.ModifyForum + Permissions.All)) == 0) throw new ForbiddenException("У вас нет прав!");
            var f = ctx.Forums.Where(f => f.ForumId == id).FirstOrDefault();
            if (f == null) throw new DoesNotExistsException($"Форума с ID {id} не существует");
            ctx.Remove(f);
            ctx.SaveChanges();
            return f;
        }

        [Authorize]
        public Models.Thread CreateThread(
                ThreadCreateInput input,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            var f = ctx.Forums.Where(f => f.ForumId == input.ForumId).FirstOrDefault();
            if (f == null) throw new DoesNotExistsException($"Форума с ID {input.ForumId} не существует");
            if (f.IsOpen == false && (user.Permissions & (Permissions.ModifyThread + Permissions.All)) == 0) throw new ForbiddenException("У вас нет прав!");

            if (input.Name.Length < 1 || input.Name.Length > 128)
            {
                throw new UserInputException("Длина названия должна быть от 1 до 128");
            }

            if (input.Message.Length < 1 || input.Message.Length > 20000)
            {
                throw new UserInputException("Длина сообщения должна быть от 1 до 20000");
            }
            var t = ctx.Threads.Add(new Models.Thread
            {
                Name = input.Name,
                ForumId = input.ForumId,
                CanChat = f.IsOpen ? true : input.CanChat,
                IsPinned = f.IsOpen ? false : input.IsPinned,
            });
            ctx.SaveChanges();
            var p = ctx.Posts.Add(new Post
            {
                ThreadId = t.Entity.ThreadId,
                OwnerId = user.UserId,
                Message = input.Message,
            });
            ctx.SaveChanges();
            return t.Entity;
        }

        [Authorize]
        public Models.Thread EditThread(
                ThreadEditInput input,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            if ((user.Permissions & (Permissions.ModifyThread + Permissions.All)) == 0) throw new ForbiddenException("У вас нет прав!");
            var t = ctx.Threads.Where(t => t.ThreadId == input.Id).FirstOrDefault();
            if (t == null) throw new DoesNotExistsException($"Темы с ID {input.Id} не существует");

            if (input.IsPinned != null)
            {
                t.IsPinned = input.IsPinned.Value;
            }

            if (input.CanChat != null)
            {
                t.CanChat = input.CanChat.Value;
            }

            if (input.Name != null)
            {
                t.Name = input.Name;
            }
            ctx.SaveChanges();
            return t;
        }

        [Authorize]
        public Models.Thread RemoveThread(
                int id,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            if ((user.Permissions & (Permissions.ModifyThread + Permissions.All)) == 0) throw new ForbiddenException("У вас нет прав!");
            var t = ctx.Threads.Where(t => t.ThreadId == id).FirstOrDefault();
            if (t == null) throw new DoesNotExistsException($"Темы с ID {id} не существует");
            ctx.Remove(t);
            ctx.SaveChanges();
            return t;
        }

        [Authorize]
        public Post CreatePost(
                PostCreateInput input,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            var t = ctx.Threads.Where(t => t.ThreadId == input.ThreadId).FirstOrDefault();
            if (t == null) throw new DoesNotExistsException("Поста не существует");
            if (t.CanChat == false)
                throw new ForbiddenException("Пост закрыт");
            if (input.Message.Length < 1 || input.Message.Length > 20000)
            {
                throw new UserInputException("Длина сообщения должна быть от 1 до 20000");
            }
            var p = ctx.Posts.Add(new Post
            {
                ThreadId = input.ThreadId,
                Message = input.Message,
                OwnerId = user.UserId,
                ReplyId = input.ReplyId
            });
            ctx.SaveChanges();
            return p.Entity;
        }

        [Authorize]
        public Post EditPost(
                PostEditInput input,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            var p = ctx.Posts.Where(p => p.PostId == input.Id).FirstOrDefault();
            if (p == null) throw new DoesNotExistsException($"Поста с ID {input.Id} не существует");
            if (p.OwnerId != user.UserId) throw new ForbiddenException("Вы не можете редактировать чужой пост");
            if (input.Message.Length < 1 || input.Message.Length > 20000)
            {
                throw new UserInputException("Длина сообщения должна быть от 1 до 20000");
            }
            p.Message = input.Message;
            ctx.SaveChanges();
            return p;
        }

        [Authorize]
        public bool IncView(
                int id,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal)
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с ID {id} не существует");
            if (user.DiscordId == long.Parse(discordId)) throw new ForbiddenException("Нельзя поднять просмотр самому себе");
            var senderUser = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (senderUser == null) return false;
            user.Views += 1;
            ctx.SaveChanges();
            return true;
        }

        [Authorize]
        public User EditUser(
                int id,
                UserEditInput input,
                [Service] gpplanetContext ctx,
                [Service] DiscordBot bot,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с ID {id} не существует");
            if (user.DiscordId != long.Parse(discordId))
            {
                var senderUser = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
                if (senderUser == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
                if (input.IsBanned != null)
                {
                    if ((bool)input.IsBanned)
                    {
                        if ((senderUser.Permissions & (Permissions.All + Permissions.SetBan)) == 0) throw new ForbiddenException("У вас нет нужных прав");
                        if (user.UserRole == UserRoleEnum.None)
                            user.IsBanned = true;
                    }
                    else
                    {
                        if ((senderUser.Permissions & (Permissions.All + Permissions.RemoveBan)) == 0) throw new ForbiddenException("У вас нет нужных прав");
                        user.IsBanned = false;
                    }
                }
                if (input.permissions != null && (senderUser.Permissions & (Permissions.All + Permissions.ModifyPermissions)) > 0)
                {
                    user.Permissions = (long)input.permissions;
                }
                if (input.role != null && (senderUser.Permissions & (Permissions.All + Permissions.ModifyRoles)) > 0)
                {
                    user.UserRole = (UserRoleEnum)input.role;
                }
            }
            else
            {
                if (input.Status != null) user.Status = input.Status;
                if (input.Description != null) user.Description = input.Description;
                if (input.Sex != null) user.Sex = (int)input.Sex;
                if (input.Avatar != null)
                {
                    if (input.Avatar == "")
                    {
                        user.Avatar = bot.GetAvatarByDiscordId(ulong.Parse(discordId));
                    }
                    else
                    {
                        if (input.Avatar.EndsWith(".gif") && (user.Permissions & (Permissions.All + Permissions.Lite + Permissions.Premium)) == 0)
                        {
                            throw new ForbiddenException("gif Аватары доступны только Lite и Premium");
                        }
                        else
                        {
                            user.Avatar = input.Avatar;
                        }
                    }
                }
                if (input.Banner != null && input.Banner.Length != 0)
                {
                    if ((user.Permissions & (Permissions.All + Permissions.Lite + Permissions.Premium)) == 0) throw new ForbiddenException("Баннеры доступны только Lite и Premium");
                    if (input.Banner.EndsWith(".gif") && (user.Permissions & (Permissions.All + Permissions.Premium)) == 0) throw new ForbiddenException("gif Баннеры доступны только Premium");
                    user.Banner = input.Banner;
                }
                if (input.IsShowPhone != null)
                {
                    if ((bool)input.IsShowPhone)
                    {
                        user.Settings |= UserSettings.ShowPhone;
                    }
                    else if ((user.Settings & UserSettings.ShowPhone) != 0)
                    {
                        user.Settings -= long.MaxValue & UserSettings.ShowPhone;
                    }
                }
                if (input.IsNotifyOnReport != null)
                {
                    if ((bool)input.IsNotifyOnReport)
                    {
                        user.Settings |= UserSettings.NotifyOnReport;
                    }
                    else if ((user.Settings & UserSettings.NotifyOnReport) != 0)
                    {
                        user.Settings -= UserSettings.NotifyOnReport;
                    }
                }
                if (input.IsNotifyOnNewSubscriber != null)
                {
                    if ((bool)input.IsNotifyOnNewSubscriber)
                    {
                        user.Settings |= UserSettings.NotifyOnNewSubscriber;
                    }
                    else if ((user.Settings & UserSettings.NotifyOnNewSubscriber) != 0)
                    {
                        user.Settings -= UserSettings.NotifyOnNewSubscriber;
                    }
                }
                if (input.IsNotifyOnNewFriend != null)
                {
                    if ((bool)input.IsNotifyOnNewFriend)
                    {
                        user.Settings |= UserSettings.NotifyOnNewFriend;
                    }
                    else if ((user.Settings & UserSettings.NotifyOnNewFriend) != 0)
                    {
                        user.Settings -= UserSettings.NotifyOnNewFriend;
                    }
                }
                if (input.IsNotifyOnReportMessage != null)
                {
                    if ((bool)input.IsNotifyOnReportMessage)
                    {
                        user.Settings |= UserSettings.NotifyOnReportMessage;
                    }
                    else if ((user.Settings & UserSettings.NotifyOnReportMessage) != 0)
                    {
                        user.Settings -= UserSettings.NotifyOnReportMessage;
                    }
                }
            }

            UserValidator validator = new();
            ValidationResult result = validator.Validate(user);
            if (result.IsValid == false)
            {
                foreach (ValidationFailure failure in result.Errors)
                {
                    throw new ValidationException(failure.ErrorMessage);
                }
            }

            ctx.SaveChanges();
            return user;
        }

        [Authorize]
        public User StartSubscribe(
                int id,
                [Service] gpplanetContext ctx,
                [Service] DiscordBot bot,
                [Service] ITopicEventSender sender,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с ID {id} не существует");
            if (user.DiscordId == long.Parse(discordId)) throw new UserInputException("Вы не можете подписаться на самого себя");
            var senderUser = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (senderUser == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            UserStatus status = GetUserStatus(senderUser.UserId, user.UserId, ctx);
            if (status == null) throw new IternalException("Проблема статуса");
            if (status.IsFriends) throw new ForbiddenException("Вы уже друзья!");
            else if (status.YouIsSubscriber) throw new ForbiddenException("Вы уже подписаны!");
            else if (status.HeIsSubscriber)
            {
                var subscriberRS = ctx.Subscribers.Where(s => s.UserId == senderUser.UserId && s.SubscriberId == user.UserId).FirstOrDefault();
                ctx.Subscribers.Remove(subscriberRS);
                ctx.Friends.Add(new Friend { UserId = senderUser.UserId, FriendId = user.UserId });
                var frs = ctx.Friends.Add(new Friend { UserId = user.UserId, FriendId = senderUser.UserId });
                ctx.SaveChanges();
                // Оповещения пользователю, с которым подружился отправитель
                var notification = ctx.Friendnotifications.Add(new Friendnotification
                {
                    ToId = user.UserId,
                    FriendRsId = frs.Entity.FriendsId,
                });
                if ((user.Settings & UserSettings.NotifyOnNewFriend) != 0)
                    bot.NotifyNewFriend(senderUser, user.DiscordId);
                ctx.SaveChanges();
                sender.SendAsync($"{user.UserId}_NewNotification", (INotification)notification.Entity).GetAwaiter().GetResult();
            }
            else
            {
                var srs = ctx.Subscribers.Add(new Subscriber { UserId = user.UserId, SubscriberId = senderUser.UserId });
                ctx.SaveChanges();
                // Оповещения пользователю, на которого подписался отправитель
                var notification = ctx.Subscribernotifications.Add(new Subscribernotification
                {
                    ToId = user.UserId,
                    SubscriberRsId = srs.Entity.SubscribersId,
                });
                if ((user.Settings & UserSettings.NotifyOnNewSubscriber) != 0)
                    bot.NotifyNewSubscriber(senderUser, user.DiscordId);
                ctx.SaveChanges();
                sender.SendAsync($"{user.UserId}_NewNotification", (INotification)notification.Entity).GetAwaiter().GetResult();
            }
            return user;
        }

        [Authorize]
        public User EndSubscribe(
                int id,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с ID {id} не существует");
            if (user.DiscordId == long.Parse(discordId)) throw new UserInputException("Вы не можете отписаться на самого себя");
            var senderUser = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (senderUser == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            UserStatus status = GetUserStatus(senderUser.UserId, user.UserId, ctx);
            if (status == null) throw new IternalException("Проблема статуса");
            if (status.IsFriends) throw new ForbiddenException("Вы друзья!");
            else if (status.YouIsSubscriber)
            {
                var subscriberRS = ctx.Subscribers.Where(s => s.UserId == user.UserId && s.SubscriberId == senderUser.UserId).FirstOrDefault();
                ctx.Subscribers.Remove(subscriberRS);
                ctx.SaveChanges();
            }

            return user;
        }

        [Authorize]
        public User RemoveFriend(
                int id,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с ID {id} не существует");
            if (user.DiscordId == long.Parse(discordId)) throw new UserInputException("Вы не можете удалить из друзей на самого себя");
            var senderUser = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (senderUser == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            UserStatus status = GetUserStatus(senderUser.UserId, user.UserId, ctx);
            if (status == null) throw new IternalException("Проблема статуса");
            if (!status.IsFriends) throw new ForbiddenException("Вы не друзья!");
            var f1 = ctx.Friends.Where(f => f.FriendId == user.UserId && f.UserId == senderUser.UserId).FirstOrDefault();
            var f2 = ctx.Friends.Where(f => f.UserId == user.UserId && f.FriendId == senderUser.UserId).FirstOrDefault();
            ctx.Remove(f1);
            ctx.Remove(f2);
            ctx.Subscribers.Add(new Subscriber
            {
                SubscriberId = user.UserId,
                UserId = senderUser.UserId,
            });
            ctx.SaveChanges();
            return user;
        }

        [Authorize]
        public User SetRating(
                int id,
                bool? positive,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с ID {id} не существует");
            if (user.DiscordId == long.Parse(discordId)) throw new ForbiddenException("Вы не можете поставить рейтинг самому себе");
            var senderUser = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (senderUser == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");

            var r = ctx.Ratings.Where(r => r.ToId == user.UserId && r.FromId == senderUser.UserId).FirstOrDefault();
            if (r != null) ctx.Ratings.Remove(r);
            if (positive != null)
            {
                ctx.Ratings.Add(new Rating
                {
                    ToId = user.UserId,
                    FromId = senderUser.UserId,
                    Positive = (bool)positive,
                });
            }
            ctx.SaveChanges();
            return user;
        }

        [Authorize]
        public Report CreateReport(
                ReportCreateInput input,
                [Service] gpplanetContext ctx,
                [Service] DiscordBot bot,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            Report.ReportSubType[] allowedReportSubTypes = new Report.ReportSubType[] { Report.ReportSubType.User, Report.ReportSubType.Admin };
            if (input.Type == Report.ReportType.Report && !allowedReportSubTypes.Contains(input.Subtype))
                throw new UserInputException("Неверный подтип для данного типа репорта");
            else if (input.Type != Report.ReportType.Report && allowedReportSubTypes.Contains(input.Subtype))
                throw new UserInputException("Неверный подтип для данного типа репорта");
            if (user.BanreportEndAt != null && user.BanreportEndAt.Value.ToUniversalTime() > DateTime.UtcNow)
                throw new ForbiddenException($"У вас бан репортов до {user.BanreportEndAt.Value.ToUniversalTime().Hour}:{user.BanreportEndAt.Value.ToUniversalTime().Minute} {user.BanreportEndAt.Value.ToUniversalTime().Day}.{user.BanreportEndAt.Value.ToUniversalTime().Month}.{user.BanreportEndAt.Value.ToUniversalTime().Year} по UTC");

            if (input.Type == Report.ReportType.Report)
            {
                if (input.ReportTo == null) throw new UserInputException("Вы не указали на кого подаете жалобу");
                var toUser = ctx.Users.Where(u => u.UserId == input.ReportTo).FirstOrDefault();
                if (toUser == null) throw new DoesNotExistsException($"Пользователя с ID {input.ReportTo} не существует");
                var reportRecord = new Report
                {
                    Type = input.Type,
                    Subtype = input.Subtype,
                    OwnerId = user.UserId,
                    ToId = toUser.UserId
                };
                reportRecord.Reportmessages.Add(new Reportmessage
                {
                    OwnerId = user.UserId,
                    Message = input.Message
                });
                var report = ctx.Reports.Add(reportRecord);
                ctx.SaveChanges();
                // Оповещения пользователю, на которого отправил репорт отправитель
                // TODO Report notification
                if ((toUser.Settings & UserSettings.NotifyOnReport) != 0)
                    bot.NotifyNewReport(report.Entity, user.DiscordId, toUser.DiscordId);
                return report.Entity;
            }
            else
            {
                var reportRecord = new Report
                {
                    Type = input.Type,
                    Subtype = input.Subtype,
                    OwnerId = user.UserId,
                };
                var m = new Reportmessage
                {
                    OwnerId = user.UserId,
                    Message = input.Message
                };
                ReportmessageValidator validator = new();
                ValidationResult result = validator.Validate(m);
                if (result.IsValid == false)
                {
                    foreach (ValidationFailure failure in result.Errors)
                    {
                        throw new ValidationException(failure.ErrorMessage);
                    }
                }
                reportRecord.Reportmessages.Add(m);
                var report = ctx.Reports.Add(reportRecord);
                ctx.SaveChanges();
                return report.Entity;
            }
        }

        [Authorize]
        public User BanReport(
                int id,
                bool isBanned,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            if (isBanned)
            {
                if ((user.Permissions & (Permissions.SetBan + Permissions.All)) == 0) throw new ForbiddenException("У вас нет прав!");
                var u = ctx.Users.Where(u => u.UserId == id).FirstOrDefault();
                if (u == null) throw new DoesNotExistsException($"Пользователя с ID {id} не существует");
                u.BanreportEndAt = DateTime.Now.ToUniversalTime().AddDays(2);
                ctx.SaveChanges();
                return u;
            }
            else
            {
                if ((user.Permissions & (Permissions.RemoveBan + Permissions.All)) == 0) throw new ForbiddenException("У вас нет прав!");
                var u = ctx.Users.Where(u => u.UserId == id).FirstOrDefault();
                if (u == null) throw new DoesNotExistsException($"Пользователя с ID {id} не существует");
                u.BanreportEndAt = null;
                ctx.SaveChanges();
                return u;
            }
        }

        [Authorize]
        public Reportmessage SendReportMessage(
                ReportSendMessageInput input,
                [Service] gpplanetContext ctx,
                [Service] DiscordBot bot,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal,
                [Service] ITopicEventSender sender
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            var report = ctx.Reports.Where(r => r.ReportId == input.ReportId).FirstOrDefault();
            if (report == null) throw new UserInputException($"Репорта с ID {input.ReportId} не существует");
            if (
                ((user.Permissions & (Permissions.All + Permissions.ShowReports)) == 0)
                &&
                (user.UserId != report.ToId && user.UserId != report.OwnerId)
                )
            {
                throw new ForbiddenException("Вы не состоите в данном репорте");
            }
            var m = new Reportmessage
            {
                ReportId = input.ReportId,
                OwnerId = user.UserId,
                Message = input.Message,
                ReplymessageId = input.ReplyMessageId
            };
            ReportmessageValidator validator = new();
            ValidationResult result = validator.Validate(m);
            if (result.IsValid == false)
            {
                foreach (ValidationFailure failure in result.Errors)
                {
                    throw new ValidationException(failure.ErrorMessage);
                }
            }
            var message = ctx.Reportmessages.Add(m);

            ctx.SaveChanges();
            sender.SendAsync($"{report.ReportId}_A_NewReportMessage", message.Entity).GetAwaiter().GetResult();
            sender.SendAsync($"{report.ReportId}_{report.OwnerId}_NewReportMessage", message.Entity).GetAwaiter().GetResult();
            if (report.ToId != null)
            {
                sender.SendAsync($"{report.ReportId}_{report.ToId}_NewReportMessage", message.Entity).GetAwaiter().GetResult();
            }
            if (user.UserId == report.ToId)
            {
                var userTo = ctx.Users.Where(u => u.UserId == report.OwnerId).FirstOrDefault();
                bot.NotifyNewReportMessage(message.Entity, userTo.DiscordId);
            }
            else if (user.UserId == report.OwnerId)
            {
                if (report.ToId != null)
                {
                    var userTo = ctx.Users.Where(u => u.UserId == report.ToId).FirstOrDefault();
                    bot.NotifyNewReportMessage(message.Entity, userTo.DiscordId);
                }
            }
            else
            {
                var userOwner = ctx.Users.Where(u => u.UserId == report.OwnerId).FirstOrDefault();
                bot.NotifyNewReportMessage(message.Entity, userOwner.DiscordId);
                if (report.ToId != null)
                {
                    var userTo = ctx.Users.Where(u => u.UserId == report.ToId).FirstOrDefault();
                    bot.NotifyNewReportMessage(message.Entity, userTo.DiscordId);
                }
            }
            return message.Entity;
        }

        [Authorize]
        public bool SetReportIsClosed(
                int reportId,
                bool isClosed,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal,
                [Service] ITopicEventSender sender
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            if ((user.Permissions & (Permissions.All + Permissions.ShowReports)) == 0) throw new ForbiddenException("У вас нет прав!");
            var report = ctx.Reports.Where(r => r.ReportId == reportId).FirstOrDefault();
            report.IsClosed = isClosed;

            ctx.SaveChanges();
            return true;
        }

        [Authorize]
        public bool ReadNotification(
                int id,
                string type,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            switch (type)
            {
                case "Systemnotification":
                    {
                        var n = ctx.Systemnotifications.Where(n => n.SystemnotificationId == id && n.ToId == user.UserId).FirstOrDefault();
                        if (n == null) throw new ForbiddenException("Вы не можете прочитать чужое оповещение");
                        ctx.Remove(n);
                        ctx.SaveChanges();
                        break;
                    }
                case "Subscribernotification":
                    {
                        var n = ctx.Subscribernotifications.Where(n => n.SubscribernotificationId == id && n.ToId == user.UserId).FirstOrDefault();
                        if (n == null) throw new ForbiddenException("Вы не можете прочитать чужое оповещение");
                        ctx.Remove(n);
                        ctx.SaveChanges();
                        break;
                    }
                case "Friendnotification":
                    {
                        var n = ctx.Friendnotifications.Where(n => n.FriendnotificationId == id && n.ToId == user.UserId).FirstOrDefault();
                        if (n == null) throw new ForbiddenException("Вы не можете прочитать чужое оповещение");
                        ctx.Remove(n);
                        ctx.SaveChanges();
                        break;
                    }
                case "Billnotification":
                    {
                        var n = ctx.Billnotifications.Where(n => n.BillnotificationId == id && n.ToId == user.UserId).FirstOrDefault();
                        if (n == null) throw new ForbiddenException("Вы не можете прочитать чужое оповещение");
                        ctx.Remove(n);
                        ctx.SaveChanges();
                        break;
                    }
                default:
                    {
                        throw new UserInputException("Данного типа не существует");
                    }
            }

            return true;
        }

        [Authorize]
        public async Task<Donateitem> BuyDonate(
                int id,
                int? amount,
                [Service] gpplanetContext ctx,
                [Service] LimeService lime,
                [Service] DiscordBot bot,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var boughtItem = ctx.Donateitems.Where(i => i.DonateitemId == id).FirstOrDefault();
            if (boughtItem == null) throw new DoesNotExistsException($"Доната с ID {id} не существует");
            if (!boughtItem.IsShow) throw new DoesNotExistsException($"Доната с ID {id} не существует");
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");


            if (boughtItem.Type == ((int)DonateTypes.Case))
            {
                if (boughtItem.Cost > user.Money) throw new ForbiddenException("У вас недостаточно монет");
                user.Money -= boughtItem.Cost;

                var lootItems = ctx.Loottables.Where(l => l.CaseId == boughtItem.DonateitemId).ToArray();
                if (lootItems == null) throw new ForbiddenException("Кейс пуст");
                var itemId = Loottable.getRandomItemId(lootItems);
                var lootedItem = ctx.Donateitems.Where(d => d.DonateitemId == itemId).FirstOrDefault();
                if (lootedItem == null) throw new DoesNotExistsException("Выпавший предмет не найден. Сообщите администрации");

                if (lootedItem.Type == ((int)DonateTypes.Money))
                {
                    lootedItem.Amount = new Random().Next(15) + 15;
                    user.Money += lootedItem.Amount;
                }
                else if (lootedItem.Type == ((int)DonateTypes.Premium))
                {
                    if ((user.Permissions & Permissions.Lite) != 0)
                    {
                        user.Permissions -= Permissions.Lite;
                        user.SubscriptionEndAt = null;
                    }
                    if (lootedItem.Amount > 0)
                    {
                        if (user.SubscriptionEndAt != null)
                        {
                            user.SubscriptionEndAt = user.SubscriptionEndAt.Value.AddDays(lootedItem.Amount).ToUniversalTime();
                        }
                        else
                        {
                            user.SubscriptionEndAt = DateTime.Now.AddDays(lootedItem.Amount).ToUniversalTime();
                        }
                    }
                    user.Permissions |= Permissions.Premium;
                    await bot.SetRole(user.DiscordId, true);
                }
                else if (lootedItem.Type == ((int)DonateTypes.Lite))
                {
                    if ((user.Permissions & Permissions.Premium) != 0)
                    {
                        user.Permissions -= Permissions.Premium;
                        user.SubscriptionEndAt = null;
                    }
                    if (lootedItem.Amount > 0)
                    {
                        if (user.SubscriptionEndAt != null)
                        {
                            user.SubscriptionEndAt = user.SubscriptionEndAt.Value.AddDays(lootedItem.Amount).ToUniversalTime();
                        }
                        else
                        {
                            user.SubscriptionEndAt = DateTime.Now.AddDays(lootedItem.Amount).ToUniversalTime();
                        }
                    }
                    user.Permissions |= Permissions.Lite;
                    await bot.SetRole(user.DiscordId, false);
                }
                else
                {
                    throw new ForbiddenException("Ошибка. Выпавший предмет вам ничего не дает. Сообщите администрации");
                }

                if (
                    lootedItem.Type == ((int)DonateTypes.Money))
                {
                    bot.Notify($"Пользователь с ID {user.UserId} приобрел {boughtItem.Name} и получил {lootedItem.Name} {lootedItem.Amount}");
                    ctx.Donatelogs.Add(new Donatelog
                    {
                        UserId = user.UserId,
                        BoughtItemId = boughtItem.DonateitemId,
                        LootItemId = lootedItem.DonateitemId,
                        Amount = lootedItem.Amount, // Кол-во конечного продукта
                    });
                }
                else
                {
                    bot.Notify($"Пользователь с ID {user.UserId} приобрел {boughtItem.Name} и получил {lootedItem.Name}");
                    ctx.Donatelogs.Add(new Donatelog
                    {
                        UserId = user.UserId,
                        BoughtItemId = boughtItem.DonateitemId,
                        LootItemId = lootedItem.DonateitemId,
                        Amount = 1, // Кол-во конечного продукта
                    });
                }
                ctx.SaveChanges();
                return lootedItem;
            }
            else if (boughtItem.Type == ((int)DonateTypes.Premium))
            {
                if (boughtItem.Cost > user.Money) throw new ForbiddenException("У вас недостаточно монет");
                user.Money -= boughtItem.Cost;
                if ((user.Permissions & Permissions.Lite) != 0)
                {
                    user.Permissions -= Permissions.Lite;
                    user.SubscriptionEndAt = null;
                }
                if (user.SubscriptionEndAt != null)
                {
                    user.SubscriptionEndAt = user.SubscriptionEndAt.Value.AddDays(boughtItem.Amount).ToUniversalTime();
                }
                else
                {
                    user.SubscriptionEndAt = DateTime.Now.AddDays(boughtItem.Amount).ToUniversalTime();
                }
                user.Permissions |= Permissions.Premium;
                ctx.Donatelogs.Add(new Donatelog
                {
                    UserId = user.UserId,
                    BoughtItemId = boughtItem.DonateitemId,
                    Amount = 1, // Кол-во конечного продукта
                });
                bot.Notify($"Пользователь с ID {user.UserId} приобрел {boughtItem.Name}");
                await bot.SetRole(user.DiscordId, true);
                ctx.SaveChanges();
                return boughtItem;
            }
            else if (boughtItem.Type == ((int)DonateTypes.Lite))
            {
                if (boughtItem.Cost > user.Money) throw new ForbiddenException("У вас недостаточно монет");
                user.Money -= boughtItem.Cost;
                if ((user.Permissions & Permissions.Premium) != 0)
                {
                    user.Permissions -= Permissions.Premium;
                    user.SubscriptionEndAt = null;
                }
                if (user.SubscriptionEndAt != null)
                {
                    user.SubscriptionEndAt = user.SubscriptionEndAt.Value.AddDays(boughtItem.Amount).ToUniversalTime();
                }
                else
                {
                    user.SubscriptionEndAt = DateTime.Now.AddDays(boughtItem.Amount).ToUniversalTime();
                }
                user.Permissions |= Permissions.Lite;
                ctx.Donatelogs.Add(new Donatelog
                {
                    UserId = user.UserId,
                    BoughtItemId = boughtItem.DonateitemId,
                    Amount = 1, // Кол-во конечного продукта
                });
                bot.Notify($"Пользователь с ID {user.UserId} приобрел {boughtItem.Name}");
                await bot.SetRole(user.DiscordId, false);
                ctx.SaveChanges();
                return boughtItem;
            }
            else if (boughtItem.Type == ((int)DonateTypes.Trefs))
            {
                if (amount == null) throw new UserInputException("Вы не указали кол-во потраченных монет");
                if (boughtItem.Cost * amount > user.Money) throw new ForbiddenException("У вас недостаточно монет");
                user.Money -= boughtItem.Cost * (int)amount;
                ctx.SaveChanges();
                await lime.Donate(user.DiscordId, (int)amount);
                bot.Notify($"Пользователь с ID {user.UserId} приобрел {boughtItem.Name} в количестве {amount} и получил {amount} треф");
                ctx.Donatelogs.Add(new Donatelog
                {
                    UserId = user.UserId,
                    BoughtItemId = boughtItem.DonateitemId,
                    Amount = (int)amount, // Кол-во конечного продукта
                });
                ctx.SaveChanges();
                return boughtItem;
            }
            else
            {
                throw new DoesNotExistsException("Неверный тип купленного предмета. Сообщите администрации");
            }
        }

    }
}
