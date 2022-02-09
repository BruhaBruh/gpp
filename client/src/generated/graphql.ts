import type { OperationStore } from '@urql/svelte';
import gql from 'graphql-tag';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: any;
  /** The `Long` scalar type represents non-fractional signed whole 64-bit numeric values. Long can represent values between -(2^63) and 2^63 - 1. */
  Long: any;
  /** Прямая ссылка на медиафайл сервисов postimg/imgur/tenor. Поддерживаются jpeg/jpg/png/gif/webp */
  MediaLink: any;
};

export enum ApplyPolicy {
  AfterResolver = 'AFTER_RESOLVER',
  BeforeResolver = 'BEFORE_RESOLVER'
}

export type Bill = {
  __typename?: 'Bill';
  amount: Scalars['Int'];
  billId: Scalars['Int'];
  billnotifications: Array<Billnotification>;
  completedAt?: Maybe<Scalars['DateTime']>;
  createdAt: Scalars['DateTime'];
  status: Scalars['String'];
  user: User;
  userId: Scalars['Int'];
};

export type BillFilterInput = {
  amount?: InputMaybe<ComparableInt32OperationFilterInput>;
  and?: InputMaybe<Array<BillFilterInput>>;
  billId?: InputMaybe<ComparableInt32OperationFilterInput>;
  billnotifications?: InputMaybe<ListFilterInputTypeOfBillnotificationFilterInput>;
  completedAt?: InputMaybe<ComparableNullableOfDateTimeOperationFilterInput>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  or?: InputMaybe<Array<BillFilterInput>>;
  status?: InputMaybe<StringOperationFilterInput>;
  user?: InputMaybe<UserFilterInput>;
  userId?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type Billnotification = {
  __typename?: 'Billnotification';
  bill: Bill;
  billId: Scalars['Int'];
  billnotificationId: Scalars['Int'];
  createdAt: Scalars['DateTime'];
  to: User;
  toId: Scalars['Int'];
};

export type BillnotificationFilterInput = {
  and?: InputMaybe<Array<BillnotificationFilterInput>>;
  bill?: InputMaybe<BillFilterInput>;
  billId?: InputMaybe<ComparableInt32OperationFilterInput>;
  billnotificationId?: InputMaybe<ComparableInt32OperationFilterInput>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  or?: InputMaybe<Array<BillnotificationFilterInput>>;
  to?: InputMaybe<UserFilterInput>;
  toId?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type BooleanOperationFilterInput = {
  eq?: InputMaybe<Scalars['Boolean']>;
  neq?: InputMaybe<Scalars['Boolean']>;
};

export type ComparableDateTimeOperationFilterInput = {
  eq?: InputMaybe<Scalars['DateTime']>;
  gt?: InputMaybe<Scalars['DateTime']>;
  gte?: InputMaybe<Scalars['DateTime']>;
  in?: InputMaybe<Array<Scalars['DateTime']>>;
  lt?: InputMaybe<Scalars['DateTime']>;
  lte?: InputMaybe<Scalars['DateTime']>;
  neq?: InputMaybe<Scalars['DateTime']>;
  ngt?: InputMaybe<Scalars['DateTime']>;
  ngte?: InputMaybe<Scalars['DateTime']>;
  nin?: InputMaybe<Array<Scalars['DateTime']>>;
  nlt?: InputMaybe<Scalars['DateTime']>;
  nlte?: InputMaybe<Scalars['DateTime']>;
};

export type ComparableInt32OperationFilterInput = {
  eq?: InputMaybe<Scalars['Int']>;
  gt?: InputMaybe<Scalars['Int']>;
  gte?: InputMaybe<Scalars['Int']>;
  in?: InputMaybe<Array<Scalars['Int']>>;
  lt?: InputMaybe<Scalars['Int']>;
  lte?: InputMaybe<Scalars['Int']>;
  neq?: InputMaybe<Scalars['Int']>;
  ngt?: InputMaybe<Scalars['Int']>;
  ngte?: InputMaybe<Scalars['Int']>;
  nin?: InputMaybe<Array<Scalars['Int']>>;
  nlt?: InputMaybe<Scalars['Int']>;
  nlte?: InputMaybe<Scalars['Int']>;
};

export type ComparableInt64OperationFilterInput = {
  eq?: InputMaybe<Scalars['Long']>;
  gt?: InputMaybe<Scalars['Long']>;
  gte?: InputMaybe<Scalars['Long']>;
  in?: InputMaybe<Array<Scalars['Long']>>;
  lt?: InputMaybe<Scalars['Long']>;
  lte?: InputMaybe<Scalars['Long']>;
  neq?: InputMaybe<Scalars['Long']>;
  ngt?: InputMaybe<Scalars['Long']>;
  ngte?: InputMaybe<Scalars['Long']>;
  nin?: InputMaybe<Array<Scalars['Long']>>;
  nlt?: InputMaybe<Scalars['Long']>;
  nlte?: InputMaybe<Scalars['Long']>;
};

export type ComparableNullableOfDateTimeOperationFilterInput = {
  eq?: InputMaybe<Scalars['DateTime']>;
  gt?: InputMaybe<Scalars['DateTime']>;
  gte?: InputMaybe<Scalars['DateTime']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['DateTime']>>>;
  lt?: InputMaybe<Scalars['DateTime']>;
  lte?: InputMaybe<Scalars['DateTime']>;
  neq?: InputMaybe<Scalars['DateTime']>;
  ngt?: InputMaybe<Scalars['DateTime']>;
  ngte?: InputMaybe<Scalars['DateTime']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['DateTime']>>>;
  nlt?: InputMaybe<Scalars['DateTime']>;
  nlte?: InputMaybe<Scalars['DateTime']>;
};

export type ComparableNullableOfInt32OperationFilterInput = {
  eq?: InputMaybe<Scalars['Int']>;
  gt?: InputMaybe<Scalars['Int']>;
  gte?: InputMaybe<Scalars['Int']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Int']>>>;
  lt?: InputMaybe<Scalars['Int']>;
  lte?: InputMaybe<Scalars['Int']>;
  neq?: InputMaybe<Scalars['Int']>;
  ngt?: InputMaybe<Scalars['Int']>;
  ngte?: InputMaybe<Scalars['Int']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Int']>>>;
  nlt?: InputMaybe<Scalars['Int']>;
  nlte?: InputMaybe<Scalars['Int']>;
};

/** A connection to a list of items. */
export type DonateItemsConnection = {
  __typename?: 'DonateItemsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<DonateItemsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Donateitem>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  totalCount: Scalars['Int'];
};

/** An edge in a connection. */
export type DonateItemsEdge = {
  __typename?: 'DonateItemsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: Donateitem;
};

export type Donateitem = {
  __typename?: 'Donateitem';
  amount: Scalars['Int'];
  cost: Scalars['Int'];
  description: Scalars['String'];
  donateitemId: Scalars['Int'];
  donatelogBoughtItems: Array<Donatelog>;
  donatelogLootItems: Array<Donatelog>;
  icon?: Maybe<Scalars['Int']>;
  isShow: Scalars['Boolean'];
  loottableCases: Array<Loottable>;
  loottableItems: Array<Loottable>;
  name: Scalars['String'];
  type: Scalars['Int'];
};

export type DonateitemFilterInput = {
  amount?: InputMaybe<ComparableInt32OperationFilterInput>;
  and?: InputMaybe<Array<DonateitemFilterInput>>;
  cost?: InputMaybe<ComparableInt32OperationFilterInput>;
  description?: InputMaybe<StringOperationFilterInput>;
  donateitemId?: InputMaybe<ComparableInt32OperationFilterInput>;
  donatelogBoughtItems?: InputMaybe<ListFilterInputTypeOfDonatelogFilterInput>;
  donatelogLootItems?: InputMaybe<ListFilterInputTypeOfDonatelogFilterInput>;
  icon?: InputMaybe<ComparableNullableOfInt32OperationFilterInput>;
  isShow?: InputMaybe<BooleanOperationFilterInput>;
  loottableCases?: InputMaybe<ListFilterInputTypeOfLoottableFilterInput>;
  loottableItems?: InputMaybe<ListFilterInputTypeOfLoottableFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<DonateitemFilterInput>>;
  type?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type DonateitemSortInput = {
  amount?: InputMaybe<SortEnumType>;
  cost?: InputMaybe<SortEnumType>;
  description?: InputMaybe<SortEnumType>;
  donateitemId?: InputMaybe<SortEnumType>;
  icon?: InputMaybe<SortEnumType>;
  isShow?: InputMaybe<SortEnumType>;
  name?: InputMaybe<SortEnumType>;
  type?: InputMaybe<SortEnumType>;
};

export type Donatelog = {
  __typename?: 'Donatelog';
  amount: Scalars['Int'];
  boughtItem: Donateitem;
  boughtItemId: Scalars['Int'];
  createdAt: Scalars['DateTime'];
  donatelogId: Scalars['Int'];
  lootItem?: Maybe<Donateitem>;
  lootItemId?: Maybe<Scalars['Int']>;
  user: User;
  userId: Scalars['Int'];
};

export type DonatelogFilterInput = {
  amount?: InputMaybe<ComparableInt32OperationFilterInput>;
  and?: InputMaybe<Array<DonatelogFilterInput>>;
  boughtItem?: InputMaybe<DonateitemFilterInput>;
  boughtItemId?: InputMaybe<ComparableInt32OperationFilterInput>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  donatelogId?: InputMaybe<ComparableInt32OperationFilterInput>;
  lootItem?: InputMaybe<DonateitemFilterInput>;
  lootItemId?: InputMaybe<ComparableNullableOfInt32OperationFilterInput>;
  or?: InputMaybe<Array<DonatelogFilterInput>>;
  user?: InputMaybe<UserFilterInput>;
  userId?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type Forum = {
  __typename?: 'Forum';
  forumId: Scalars['Int'];
  inverseParentForum: Array<Forum>;
  isOpen: Scalars['Boolean'];
  link?: Maybe<Scalars['String']>;
  name: Scalars['String'];
  parentForum?: Maybe<Forum>;
  parentForumId?: Maybe<Scalars['Int']>;
  threads: Array<Thread>;
};

export type ForumCreateInput = {
  isOpen: Scalars['Boolean'];
  link?: InputMaybe<Scalars['String']>;
  name: Scalars['String'];
  parentForumId: Scalars['Int'];
};

export type ForumEditInput = {
  id: Scalars['Int'];
  link?: InputMaybe<Scalars['String']>;
  name?: InputMaybe<Scalars['String']>;
  parentForumId?: InputMaybe<Scalars['Int']>;
};

export type ForumFilterInput = {
  and?: InputMaybe<Array<ForumFilterInput>>;
  forumId?: InputMaybe<ComparableInt32OperationFilterInput>;
  inverseParentForum?: InputMaybe<ListFilterInputTypeOfForumFilterInput>;
  isOpen?: InputMaybe<BooleanOperationFilterInput>;
  link?: InputMaybe<StringOperationFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<ForumFilterInput>>;
  parentForum?: InputMaybe<ForumFilterInput>;
  parentForumId?: InputMaybe<ComparableNullableOfInt32OperationFilterInput>;
  threads?: InputMaybe<ListFilterInputTypeOfThreadFilterInput>;
};

export type ForumSortInput = {
  forumId?: InputMaybe<SortEnumType>;
  isOpen?: InputMaybe<SortEnumType>;
  link?: InputMaybe<SortEnumType>;
  name?: InputMaybe<SortEnumType>;
  parentForum?: InputMaybe<ForumSortInput>;
  parentForumId?: InputMaybe<SortEnumType>;
};

export type Friend = {
  __typename?: 'Friend';
  friendId: Scalars['Int'];
  friendNavigation: User;
  friendnotifications: Array<Friendnotification>;
  friendsId: Scalars['Int'];
  user: User;
  userId: Scalars['Int'];
};

export type FriendFilterInput = {
  and?: InputMaybe<Array<FriendFilterInput>>;
  friendId?: InputMaybe<ComparableInt32OperationFilterInput>;
  friendNavigation?: InputMaybe<UserFilterInput>;
  friendnotifications?: InputMaybe<ListFilterInputTypeOfFriendnotificationFilterInput>;
  friendsId?: InputMaybe<ComparableInt32OperationFilterInput>;
  or?: InputMaybe<Array<FriendFilterInput>>;
  user?: InputMaybe<UserFilterInput>;
  userId?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type Friendnotification = {
  __typename?: 'Friendnotification';
  createdAt: Scalars['DateTime'];
  friendRs: Friend;
  friendRsId: Scalars['Int'];
  friendnotificationId: Scalars['Int'];
  to: User;
  toId: Scalars['Int'];
};

export type FriendnotificationFilterInput = {
  and?: InputMaybe<Array<FriendnotificationFilterInput>>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  friendRs?: InputMaybe<FriendFilterInput>;
  friendRsId?: InputMaybe<ComparableInt32OperationFilterInput>;
  friendnotificationId?: InputMaybe<ComparableInt32OperationFilterInput>;
  or?: InputMaybe<Array<FriendnotificationFilterInput>>;
  to?: InputMaybe<UserFilterInput>;
  toId?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type ListFilterInputTypeOfBillFilterInput = {
  all?: InputMaybe<BillFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<BillFilterInput>;
  some?: InputMaybe<BillFilterInput>;
};

export type ListFilterInputTypeOfBillnotificationFilterInput = {
  all?: InputMaybe<BillnotificationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<BillnotificationFilterInput>;
  some?: InputMaybe<BillnotificationFilterInput>;
};

export type ListFilterInputTypeOfDonatelogFilterInput = {
  all?: InputMaybe<DonatelogFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<DonatelogFilterInput>;
  some?: InputMaybe<DonatelogFilterInput>;
};

export type ListFilterInputTypeOfForumFilterInput = {
  all?: InputMaybe<ForumFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<ForumFilterInput>;
  some?: InputMaybe<ForumFilterInput>;
};

export type ListFilterInputTypeOfFriendFilterInput = {
  all?: InputMaybe<FriendFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<FriendFilterInput>;
  some?: InputMaybe<FriendFilterInput>;
};

export type ListFilterInputTypeOfFriendnotificationFilterInput = {
  all?: InputMaybe<FriendnotificationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<FriendnotificationFilterInput>;
  some?: InputMaybe<FriendnotificationFilterInput>;
};

export type ListFilterInputTypeOfLoottableFilterInput = {
  all?: InputMaybe<LoottableFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<LoottableFilterInput>;
  some?: InputMaybe<LoottableFilterInput>;
};

export type ListFilterInputTypeOfPostFilterInput = {
  all?: InputMaybe<PostFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<PostFilterInput>;
  some?: InputMaybe<PostFilterInput>;
};

export type ListFilterInputTypeOfRatingFilterInput = {
  all?: InputMaybe<RatingFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<RatingFilterInput>;
  some?: InputMaybe<RatingFilterInput>;
};

export type ListFilterInputTypeOfReportFilterInput = {
  all?: InputMaybe<ReportFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<ReportFilterInput>;
  some?: InputMaybe<ReportFilterInput>;
};

export type ListFilterInputTypeOfReportmessageFilterInput = {
  all?: InputMaybe<ReportmessageFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<ReportmessageFilterInput>;
  some?: InputMaybe<ReportmessageFilterInput>;
};

export type ListFilterInputTypeOfSubscriberFilterInput = {
  all?: InputMaybe<SubscriberFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<SubscriberFilterInput>;
  some?: InputMaybe<SubscriberFilterInput>;
};

export type ListFilterInputTypeOfSubscribernotificationFilterInput = {
  all?: InputMaybe<SubscribernotificationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<SubscribernotificationFilterInput>;
  some?: InputMaybe<SubscribernotificationFilterInput>;
};

export type ListFilterInputTypeOfSystemnotificationFilterInput = {
  all?: InputMaybe<SystemnotificationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<SystemnotificationFilterInput>;
  some?: InputMaybe<SystemnotificationFilterInput>;
};

export type ListFilterInputTypeOfThreadFilterInput = {
  all?: InputMaybe<ThreadFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<ThreadFilterInput>;
  some?: InputMaybe<ThreadFilterInput>;
};

export type ListFilterInputTypeOfUserDiscordRoleFilterInput = {
  all?: InputMaybe<UserDiscordRoleFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<UserDiscordRoleFilterInput>;
  some?: InputMaybe<UserDiscordRoleFilterInput>;
};

export type Loottable = {
  __typename?: 'Loottable';
  case: Donateitem;
  caseId: Scalars['Int'];
  item: Donateitem;
  itemId: Scalars['Int'];
  loottableId: Scalars['Int'];
  weight: Scalars['Int'];
};

export type LoottableFilterInput = {
  and?: InputMaybe<Array<LoottableFilterInput>>;
  case?: InputMaybe<DonateitemFilterInput>;
  caseId?: InputMaybe<ComparableInt32OperationFilterInput>;
  item?: InputMaybe<DonateitemFilterInput>;
  itemId?: InputMaybe<ComparableInt32OperationFilterInput>;
  loottableId?: InputMaybe<ComparableInt32OperationFilterInput>;
  or?: InputMaybe<Array<LoottableFilterInput>>;
  weight?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type Mutation = {
  __typename?: 'Mutation';
  addSocialPoints: User;
  banReport: User;
  buyDonate: Donateitem;
  createForum: Forum;
  createPost: Post;
  createReport: Report;
  createThread: Thread;
  editForum: Forum;
  editPost: Post;
  editThread: Thread;
  editUser: User;
  endSubscribe: User;
  incView: Scalars['Boolean'];
  initialUsersAdd: Scalars['Boolean'];
  login: Scalars['Boolean'];
  readNotification: Scalars['Boolean'];
  removeForum: Forum;
  removeFriend: User;
  removeThread: Thread;
  sendReportMessage: Reportmessage;
  sendSystemNotification: Scalars['Boolean'];
  setRating: User;
  setReportIsClosed: Scalars['Boolean'];
  startSubscribe: User;
  test: Scalars['Boolean'];
};


export type MutationAddSocialPointsArgs = {
  id: Scalars['Int'];
  socialPoints: Scalars['Int'];
};


export type MutationBanReportArgs = {
  id: Scalars['Int'];
  isBanned: Scalars['Boolean'];
};


export type MutationBuyDonateArgs = {
  amount?: InputMaybe<Scalars['Int']>;
  id: Scalars['Int'];
};


export type MutationCreateForumArgs = {
  input: ForumCreateInput;
};


export type MutationCreatePostArgs = {
  input: PostCreateInput;
};


export type MutationCreateReportArgs = {
  input: ReportCreateInput;
};


export type MutationCreateThreadArgs = {
  input: ThreadCreateInput;
};


export type MutationEditForumArgs = {
  input: ForumEditInput;
};


export type MutationEditPostArgs = {
  input: PostEditInput;
};


export type MutationEditThreadArgs = {
  input: ThreadEditInput;
};


export type MutationEditUserArgs = {
  id: Scalars['Int'];
  input: UserEditInput;
};


export type MutationEndSubscribeArgs = {
  id: Scalars['Int'];
};


export type MutationIncViewArgs = {
  id: Scalars['Int'];
};


export type MutationReadNotificationArgs = {
  id: Scalars['Int'];
  type: Scalars['String'];
};


export type MutationRemoveForumArgs = {
  id: Scalars['Int'];
};


export type MutationRemoveFriendArgs = {
  id: Scalars['Int'];
};


export type MutationRemoveThreadArgs = {
  id: Scalars['Int'];
};


export type MutationSendReportMessageArgs = {
  input: ReportSendMessageInput;
};


export type MutationSendSystemNotificationArgs = {
  message: Scalars['String'];
  toid?: InputMaybe<Scalars['Int']>;
};


export type MutationSetRatingArgs = {
  id: Scalars['Int'];
  positive?: InputMaybe<Scalars['Boolean']>;
};


export type MutationSetReportIsClosedArgs = {
  isClosed: Scalars['Boolean'];
  reportId: Scalars['Int'];
};


export type MutationStartSubscribeArgs = {
  id: Scalars['Int'];
};

export type NewPlayer = {
  __typename?: 'NewPlayer';
  inc: Scalars['Int'];
  time: Scalars['DateTime'];
  total: Scalars['Int'];
};

export type Notification = Billnotification | Friendnotification | Subscribernotification | Systemnotification;

export type Online = {
  __typename?: 'Online';
  avg: Scalars['Int'];
  max: Scalars['Int'];
  min: Scalars['Int'];
  time: Scalars['DateTime'];
};

export enum OnlineTypes {
  Day = 'DAY',
  Hour = 'HOUR',
  Month = 'MONTH',
  Week = 'WEEK'
}

/** Information about pagination in a connection. */
export type PageInfo = {
  __typename?: 'PageInfo';
  /** When paginating forwards, the cursor to continue. */
  endCursor?: Maybe<Scalars['String']>;
  /** Indicates whether more edges exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean'];
  /** Indicates whether more edges exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean'];
  /** When paginating backwards, the cursor to continue. */
  startCursor?: Maybe<Scalars['String']>;
};

export type Post = {
  __typename?: 'Post';
  createdAt: Scalars['DateTime'];
  inverseReply: Array<Post>;
  message: Scalars['String'];
  owner?: Maybe<User>;
  ownerId: Scalars['Int'];
  postId: Scalars['Int'];
  reply?: Maybe<Post>;
  replyId?: Maybe<Scalars['Int']>;
  thread: Thread;
  threadId: Scalars['Int'];
  updatedAt: Scalars['DateTime'];
};

export type PostCreateInput = {
  message: Scalars['String'];
  replyId?: InputMaybe<Scalars['Int']>;
  threadId: Scalars['Int'];
};

export type PostEditInput = {
  id: Scalars['Int'];
  message: Scalars['String'];
};

export type PostFilterInput = {
  and?: InputMaybe<Array<PostFilterInput>>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  inverseReply?: InputMaybe<ListFilterInputTypeOfPostFilterInput>;
  message?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<PostFilterInput>>;
  owner?: InputMaybe<UserFilterInput>;
  ownerId?: InputMaybe<ComparableInt32OperationFilterInput>;
  postId?: InputMaybe<ComparableInt32OperationFilterInput>;
  reply?: InputMaybe<PostFilterInput>;
  replyId?: InputMaybe<ComparableNullableOfInt32OperationFilterInput>;
  thread?: InputMaybe<ThreadFilterInput>;
  threadId?: InputMaybe<ComparableInt32OperationFilterInput>;
  updatedAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
};

export type PostSortInput = {
  createdAt?: InputMaybe<SortEnumType>;
  message?: InputMaybe<SortEnumType>;
  owner?: InputMaybe<UserSortInput>;
  ownerId?: InputMaybe<SortEnumType>;
  postId?: InputMaybe<SortEnumType>;
  reply?: InputMaybe<PostSortInput>;
  replyId?: InputMaybe<SortEnumType>;
  thread?: InputMaybe<ThreadSortInput>;
  threadId?: InputMaybe<SortEnumType>;
  updatedAt?: InputMaybe<SortEnumType>;
};

/** A connection to a list of items. */
export type PostsConnection = {
  __typename?: 'PostsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<PostsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Post>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  totalCount: Scalars['Int'];
};

/** An edge in a connection. */
export type PostsEdge = {
  __typename?: 'PostsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: Post;
};

export type Query = {
  __typename?: 'Query';
  discordRoles: Array<UserDiscordRole>;
  donateItems?: Maybe<DonateItemsConnection>;
  forums: Array<Forum>;
  me: User;
  newPlayerLogs: Array<NewPlayer>;
  notifications: Array<Notification>;
  posts?: Maybe<PostsConnection>;
  reportMessages?: Maybe<ReportMessagesConnection>;
  reports?: Maybe<ReportsConnection>;
  serverOnlineLogs: Array<Online>;
  siteOnlineLogs: Array<Online>;
  status: UserStatus;
  threads?: Maybe<ThreadsConnection>;
  top: Array<User>;
  users?: Maybe<UsersConnection>;
};


export type QueryDonateItemsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  order?: InputMaybe<Array<DonateitemSortInput>>;
  where?: InputMaybe<DonateitemFilterInput>;
};


export type QueryForumsArgs = {
  order?: InputMaybe<Array<ForumSortInput>>;
  where?: InputMaybe<ForumFilterInput>;
};


export type QueryNewPlayerLogsArgs = {
  type: OnlineTypes;
};


export type QueryPostsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  order?: InputMaybe<Array<PostSortInput>>;
  where?: InputMaybe<PostFilterInput>;
};


export type QueryReportMessagesArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  order?: InputMaybe<Array<ReportmessageSortInput>>;
  reportId: Scalars['Int'];
  where?: InputMaybe<ReportmessageFilterInput>;
};


export type QueryReportsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  order?: InputMaybe<Array<ReportSortInput>>;
  where?: InputMaybe<ReportFilterInput>;
};


export type QueryServerOnlineLogsArgs = {
  type: OnlineTypes;
};


export type QuerySiteOnlineLogsArgs = {
  type: OnlineTypes;
};


export type QueryStatusArgs = {
  id: Scalars['Int'];
};


export type QueryThreadsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  order?: InputMaybe<Array<ThreadSortInput>>;
  where?: InputMaybe<ThreadFilterInput>;
};


export type QueryTopArgs = {
  order?: InputMaybe<Array<UserSortInput>>;
  type: UserTopEnum;
};


export type QueryUsersArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  order?: InputMaybe<Array<UserSortInput>>;
  search?: InputMaybe<Scalars['String']>;
  where?: InputMaybe<UserFilterInput>;
};

export type Rating = {
  __typename?: 'Rating';
  from: User;
  fromId: Scalars['Int'];
  positive: Scalars['Boolean'];
  ratingId: Scalars['Int'];
  to: User;
  toId: Scalars['Int'];
};

export type RatingFilterInput = {
  and?: InputMaybe<Array<RatingFilterInput>>;
  from?: InputMaybe<UserFilterInput>;
  fromId?: InputMaybe<ComparableInt32OperationFilterInput>;
  or?: InputMaybe<Array<RatingFilterInput>>;
  positive?: InputMaybe<BooleanOperationFilterInput>;
  ratingId?: InputMaybe<ComparableInt32OperationFilterInput>;
  to?: InputMaybe<UserFilterInput>;
  toId?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type Report = {
  __typename?: 'Report';
  createdAt: Scalars['DateTime'];
  isClosed: Scalars['Boolean'];
  lastMessage?: Maybe<Reportmessage>;
  owner: User;
  ownerId: Scalars['Int'];
  reportId: Scalars['Int'];
  reportmessages: Array<Reportmessage>;
  subtype: ReportSubType;
  to?: Maybe<User>;
  toId?: Maybe<Scalars['Int']>;
  type: ReportType;
};

export type ReportCreateInput = {
  message: Scalars['String'];
  reportTo?: InputMaybe<Scalars['Int']>;
  subtype: ReportSubType;
  type: ReportType;
};

export type ReportFilterInput = {
  and?: InputMaybe<Array<ReportFilterInput>>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  isClosed?: InputMaybe<BooleanOperationFilterInput>;
  lastMessage?: InputMaybe<ReportmessageFilterInput>;
  or?: InputMaybe<Array<ReportFilterInput>>;
  owner?: InputMaybe<UserFilterInput>;
  ownerId?: InputMaybe<ComparableInt32OperationFilterInput>;
  reportId?: InputMaybe<ComparableInt32OperationFilterInput>;
  reportmessages?: InputMaybe<ListFilterInputTypeOfReportmessageFilterInput>;
  subtype?: InputMaybe<ReportSubTypeOperationFilterInput>;
  to?: InputMaybe<UserFilterInput>;
  toId?: InputMaybe<ComparableNullableOfInt32OperationFilterInput>;
  type?: InputMaybe<ReportTypeOperationFilterInput>;
};

/** A connection to a list of items. */
export type ReportMessagesConnection = {
  __typename?: 'ReportMessagesConnection';
  /** A list of edges. */
  edges?: Maybe<Array<ReportMessagesEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Reportmessage>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  totalCount: Scalars['Int'];
};

/** An edge in a connection. */
export type ReportMessagesEdge = {
  __typename?: 'ReportMessagesEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: Reportmessage;
};

export type ReportSendMessageInput = {
  message: Scalars['String'];
  replyMessageId?: InputMaybe<Scalars['Int']>;
  reportId: Scalars['Int'];
};

export type ReportSortInput = {
  createdAt?: InputMaybe<SortEnumType>;
  isClosed?: InputMaybe<SortEnumType>;
  lastMessage?: InputMaybe<ReportmessageSortInput>;
  owner?: InputMaybe<UserSortInput>;
  ownerId?: InputMaybe<SortEnumType>;
  reportId?: InputMaybe<SortEnumType>;
  subtype?: InputMaybe<SortEnumType>;
  to?: InputMaybe<UserSortInput>;
  toId?: InputMaybe<SortEnumType>;
  type?: InputMaybe<SortEnumType>;
};

export enum ReportSubType {
  Admin = 'ADMIN',
  Server = 'SERVER',
  Site = 'SITE',
  User = 'USER'
}

export type ReportSubTypeOperationFilterInput = {
  eq?: InputMaybe<ReportSubType>;
  in?: InputMaybe<Array<ReportSubType>>;
  neq?: InputMaybe<ReportSubType>;
  nin?: InputMaybe<Array<ReportSubType>>;
};

export enum ReportType {
  Bug = 'BUG',
  Feature = 'FEATURE',
  Report = 'REPORT'
}

export type ReportTypeOperationFilterInput = {
  eq?: InputMaybe<ReportType>;
  in?: InputMaybe<Array<ReportType>>;
  neq?: InputMaybe<ReportType>;
  nin?: InputMaybe<Array<ReportType>>;
};

export type Reportmessage = {
  __typename?: 'Reportmessage';
  createdAt: Scalars['DateTime'];
  inverseReplymessage: Array<Reportmessage>;
  message: Scalars['String'];
  owner: User;
  ownerId: Scalars['Int'];
  replymessage?: Maybe<Reportmessage>;
  replymessageId?: Maybe<Scalars['Int']>;
  report: Report;
  reportId: Scalars['Int'];
  reportmessageId: Scalars['Int'];
};

export type ReportmessageFilterInput = {
  and?: InputMaybe<Array<ReportmessageFilterInput>>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  inverseReplymessage?: InputMaybe<ListFilterInputTypeOfReportmessageFilterInput>;
  message?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<ReportmessageFilterInput>>;
  owner?: InputMaybe<UserFilterInput>;
  ownerId?: InputMaybe<ComparableInt32OperationFilterInput>;
  replymessage?: InputMaybe<ReportmessageFilterInput>;
  replymessageId?: InputMaybe<ComparableNullableOfInt32OperationFilterInput>;
  report?: InputMaybe<ReportFilterInput>;
  reportId?: InputMaybe<ComparableInt32OperationFilterInput>;
  reportmessageId?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type ReportmessageSortInput = {
  createdAt?: InputMaybe<SortEnumType>;
  message?: InputMaybe<SortEnumType>;
  owner?: InputMaybe<UserSortInput>;
  ownerId?: InputMaybe<SortEnumType>;
  replymessage?: InputMaybe<ReportmessageSortInput>;
  replymessageId?: InputMaybe<SortEnumType>;
  report?: InputMaybe<ReportSortInput>;
  reportId?: InputMaybe<SortEnumType>;
  reportmessageId?: InputMaybe<SortEnumType>;
};

/** A connection to a list of items. */
export type ReportsConnection = {
  __typename?: 'ReportsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<ReportsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Report>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  totalCount: Scalars['Int'];
};

/** An edge in a connection. */
export type ReportsEdge = {
  __typename?: 'ReportsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: Report;
};

export enum SortEnumType {
  Asc = 'ASC',
  Desc = 'DESC'
}

export type StringOperationFilterInput = {
  and?: InputMaybe<Array<StringOperationFilterInput>>;
  contains?: InputMaybe<Scalars['String']>;
  endsWith?: InputMaybe<Scalars['String']>;
  eq?: InputMaybe<Scalars['String']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['String']>>>;
  ncontains?: InputMaybe<Scalars['String']>;
  nendsWith?: InputMaybe<Scalars['String']>;
  neq?: InputMaybe<Scalars['String']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['String']>>>;
  nstartsWith?: InputMaybe<Scalars['String']>;
  or?: InputMaybe<Array<StringOperationFilterInput>>;
  startsWith?: InputMaybe<Scalars['String']>;
};

export type Subscriber = {
  __typename?: 'Subscriber';
  subscriberId: Scalars['Int'];
  subscriberNavigation: User;
  subscribernotifications: Array<Subscribernotification>;
  subscribersId: Scalars['Int'];
  user: User;
  userId: Scalars['Int'];
};

export type SubscriberFilterInput = {
  and?: InputMaybe<Array<SubscriberFilterInput>>;
  or?: InputMaybe<Array<SubscriberFilterInput>>;
  subscriberId?: InputMaybe<ComparableInt32OperationFilterInput>;
  subscriberNavigation?: InputMaybe<UserFilterInput>;
  subscribernotifications?: InputMaybe<ListFilterInputTypeOfSubscribernotificationFilterInput>;
  subscribersId?: InputMaybe<ComparableInt32OperationFilterInput>;
  user?: InputMaybe<UserFilterInput>;
  userId?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type Subscribernotification = {
  __typename?: 'Subscribernotification';
  createdAt: Scalars['DateTime'];
  subscriberRs: Subscriber;
  subscriberRsId: Scalars['Int'];
  subscribernotificationId: Scalars['Int'];
  to: User;
  toId: Scalars['Int'];
};

export type SubscribernotificationFilterInput = {
  and?: InputMaybe<Array<SubscribernotificationFilterInput>>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  or?: InputMaybe<Array<SubscribernotificationFilterInput>>;
  subscriberRs?: InputMaybe<SubscriberFilterInput>;
  subscriberRsId?: InputMaybe<ComparableInt32OperationFilterInput>;
  subscribernotificationId?: InputMaybe<ComparableInt32OperationFilterInput>;
  to?: InputMaybe<UserFilterInput>;
  toId?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type Subscription = {
  __typename?: 'Subscription';
  newNotification: Notification;
  newReportMessage: Reportmessage;
};


export type SubscriptionNewReportMessageArgs = {
  id: Scalars['Int'];
};

export type Systemnotification = {
  __typename?: 'Systemnotification';
  createdAt: Scalars['DateTime'];
  message: Scalars['String'];
  systemnotificationId: Scalars['Int'];
  to: User;
  toId: Scalars['Int'];
};

export type SystemnotificationFilterInput = {
  and?: InputMaybe<Array<SystemnotificationFilterInput>>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  message?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<SystemnotificationFilterInput>>;
  systemnotificationId?: InputMaybe<ComparableInt32OperationFilterInput>;
  to?: InputMaybe<UserFilterInput>;
  toId?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type Thread = {
  __typename?: 'Thread';
  canChat?: Maybe<Scalars['Boolean']>;
  createdAt: Scalars['DateTime'];
  firstPost?: Maybe<Post>;
  forum: Forum;
  forumId: Scalars['Int'];
  isPinned: Scalars['Boolean'];
  lastPost?: Maybe<Post>;
  name: Scalars['String'];
  posts: Array<Post>;
  threadId: Scalars['Int'];
};

export type ThreadCreateInput = {
  canChat: Scalars['Boolean'];
  forumId: Scalars['Int'];
  isPinned: Scalars['Boolean'];
  message: Scalars['String'];
  name: Scalars['String'];
};

export type ThreadEditInput = {
  canChat?: InputMaybe<Scalars['Boolean']>;
  id: Scalars['Int'];
  isPinned?: InputMaybe<Scalars['Boolean']>;
  name?: InputMaybe<Scalars['String']>;
};

export type ThreadFilterInput = {
  and?: InputMaybe<Array<ThreadFilterInput>>;
  canChat?: InputMaybe<BooleanOperationFilterInput>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  firstPost?: InputMaybe<PostFilterInput>;
  forum?: InputMaybe<ForumFilterInput>;
  forumId?: InputMaybe<ComparableInt32OperationFilterInput>;
  isPinned?: InputMaybe<BooleanOperationFilterInput>;
  lastPost?: InputMaybe<PostFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<ThreadFilterInput>>;
  posts?: InputMaybe<ListFilterInputTypeOfPostFilterInput>;
  threadId?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type ThreadSortInput = {
  canChat?: InputMaybe<SortEnumType>;
  createdAt?: InputMaybe<SortEnumType>;
  firstPost?: InputMaybe<PostSortInput>;
  forum?: InputMaybe<ForumSortInput>;
  forumId?: InputMaybe<SortEnumType>;
  isPinned?: InputMaybe<SortEnumType>;
  lastPost?: InputMaybe<PostSortInput>;
  name?: InputMaybe<SortEnumType>;
  threadId?: InputMaybe<SortEnumType>;
};

/** A connection to a list of items. */
export type ThreadsConnection = {
  __typename?: 'ThreadsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<ThreadsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Thread>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  totalCount: Scalars['Int'];
};

/** An edge in a connection. */
export type ThreadsEdge = {
  __typename?: 'ThreadsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: Thread;
};

export type User = {
  __typename?: 'User';
  avatar: Scalars['String'];
  banner?: Maybe<Scalars['MediaLink']>;
  banreportEndAt?: Maybe<Scalars['DateTime']>;
  billnotifications: Array<Billnotification>;
  bills: Array<Bill>;
  createdAt: Scalars['DateTime'];
  description: Scalars['String'];
  discordId: Scalars['Long'];
  discordRoles: Array<UserDiscordRole>;
  donatelogs: Array<Donatelog>;
  friendFriendNavigations: Array<Friend>;
  friendUsers: Array<Friend>;
  friendnotifications: Array<Friendnotification>;
  isBanned: Scalars['Boolean'];
  lastOnline: Scalars['DateTime'];
  level: Scalars['Int'];
  money: Scalars['Int'];
  nickname: Scalars['String'];
  permissions: Scalars['Long'];
  phone?: Maybe<Scalars['Int']>;
  posts: Array<Post>;
  rating: UserRating;
  ratingFroms: Array<Rating>;
  ratingTos: Array<Rating>;
  reportOwners: Array<Report>;
  reportTos: Array<Report>;
  reportmessages: Array<Reportmessage>;
  role?: Maybe<Scalars['String']>;
  settings: Scalars['Long'];
  sex: Scalars['Int'];
  socialPoints: Scalars['Int'];
  status: Scalars['String'];
  subscriberSubscriberNavigations: Array<Subscriber>;
  subscriberUsers: Array<Subscriber>;
  subscribernotifications: Array<Subscribernotification>;
  subscriptionEndAt?: Maybe<Scalars['DateTime']>;
  systemnotifications: Array<Systemnotification>;
  tag: Scalars['String'];
  totalFriends: Scalars['Int'];
  totalSubscribers: Scalars['Int'];
  trefs?: Maybe<Scalars['Int']>;
  updatedAt: Scalars['DateTime'];
  userId: Scalars['Int'];
  userRole: UserRoleEnum;
  views: Scalars['Int'];
  work?: Maybe<Scalars['String']>;
};

export type UserDiscordRole = {
  __typename?: 'UserDiscordRole';
  color: Scalars['String'];
  hoist: Scalars['Boolean'];
  id: Scalars['Long'];
  name: Scalars['String'];
  position: Scalars['Int'];
};

export type UserDiscordRoleFilterInput = {
  and?: InputMaybe<Array<UserDiscordRoleFilterInput>>;
  color?: InputMaybe<StringOperationFilterInput>;
  hoist?: InputMaybe<BooleanOperationFilterInput>;
  id?: InputMaybe<ComparableInt64OperationFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<UserDiscordRoleFilterInput>>;
  position?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type UserEditInput = {
  avatar?: InputMaybe<Scalars['MediaLink']>;
  banner?: InputMaybe<Scalars['MediaLink']>;
  description?: InputMaybe<Scalars['String']>;
  isBanned?: InputMaybe<Scalars['Boolean']>;
  isNotifyOnNewFriend?: InputMaybe<Scalars['Boolean']>;
  isNotifyOnNewSubscriber?: InputMaybe<Scalars['Boolean']>;
  isNotifyOnReport?: InputMaybe<Scalars['Boolean']>;
  isNotifyOnReportMessage?: InputMaybe<Scalars['Boolean']>;
  isShowPhone?: InputMaybe<Scalars['Boolean']>;
  permissions?: InputMaybe<Scalars['Long']>;
  role?: InputMaybe<UserRoleEnum>;
  sex?: InputMaybe<Scalars['Int']>;
  status?: InputMaybe<Scalars['String']>;
};

export type UserFilterInput = {
  and?: InputMaybe<Array<UserFilterInput>>;
  avatar?: InputMaybe<StringOperationFilterInput>;
  banner?: InputMaybe<StringOperationFilterInput>;
  banreportEndAt?: InputMaybe<ComparableNullableOfDateTimeOperationFilterInput>;
  billnotifications?: InputMaybe<ListFilterInputTypeOfBillnotificationFilterInput>;
  bills?: InputMaybe<ListFilterInputTypeOfBillFilterInput>;
  createdAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  description?: InputMaybe<StringOperationFilterInput>;
  discordId?: InputMaybe<ComparableInt64OperationFilterInput>;
  discordRoles?: InputMaybe<ListFilterInputTypeOfUserDiscordRoleFilterInput>;
  donatelogs?: InputMaybe<ListFilterInputTypeOfDonatelogFilterInput>;
  friendFriendNavigations?: InputMaybe<ListFilterInputTypeOfFriendFilterInput>;
  friendUsers?: InputMaybe<ListFilterInputTypeOfFriendFilterInput>;
  friendnotifications?: InputMaybe<ListFilterInputTypeOfFriendnotificationFilterInput>;
  isBanned?: InputMaybe<BooleanOperationFilterInput>;
  lastOnline?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  level?: InputMaybe<ComparableInt32OperationFilterInput>;
  money?: InputMaybe<ComparableInt32OperationFilterInput>;
  nickname?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<UserFilterInput>>;
  permissions?: InputMaybe<ComparableInt64OperationFilterInput>;
  phone?: InputMaybe<ComparableNullableOfInt32OperationFilterInput>;
  posts?: InputMaybe<ListFilterInputTypeOfPostFilterInput>;
  rating?: InputMaybe<UserRatingFilterInput>;
  ratingFroms?: InputMaybe<ListFilterInputTypeOfRatingFilterInput>;
  ratingTos?: InputMaybe<ListFilterInputTypeOfRatingFilterInput>;
  reportOwners?: InputMaybe<ListFilterInputTypeOfReportFilterInput>;
  reportTos?: InputMaybe<ListFilterInputTypeOfReportFilterInput>;
  reportmessages?: InputMaybe<ListFilterInputTypeOfReportmessageFilterInput>;
  role?: InputMaybe<StringOperationFilterInput>;
  settings?: InputMaybe<ComparableInt64OperationFilterInput>;
  sex?: InputMaybe<ComparableInt32OperationFilterInput>;
  socialPoints?: InputMaybe<ComparableInt32OperationFilterInput>;
  status?: InputMaybe<StringOperationFilterInput>;
  subscriberSubscriberNavigations?: InputMaybe<ListFilterInputTypeOfSubscriberFilterInput>;
  subscriberUsers?: InputMaybe<ListFilterInputTypeOfSubscriberFilterInput>;
  subscribernotifications?: InputMaybe<ListFilterInputTypeOfSubscribernotificationFilterInput>;
  subscriptionEndAt?: InputMaybe<ComparableNullableOfDateTimeOperationFilterInput>;
  systemnotifications?: InputMaybe<ListFilterInputTypeOfSystemnotificationFilterInput>;
  tag?: InputMaybe<StringOperationFilterInput>;
  totalFriends?: InputMaybe<ComparableNullableOfInt32OperationFilterInput>;
  totalSubscribers?: InputMaybe<ComparableNullableOfInt32OperationFilterInput>;
  trefs?: InputMaybe<ComparableNullableOfInt32OperationFilterInput>;
  updatedAt?: InputMaybe<ComparableDateTimeOperationFilterInput>;
  userId?: InputMaybe<ComparableInt32OperationFilterInput>;
  userRole?: InputMaybe<UserRoleEnumOperationFilterInput>;
  views?: InputMaybe<ComparableInt32OperationFilterInput>;
  work?: InputMaybe<StringOperationFilterInput>;
};

export type UserRating = {
  __typename?: 'UserRating';
  negative: Scalars['Int'];
  positive: Scalars['Int'];
  result: Scalars['Int'];
  total: Scalars['Int'];
  your: Scalars['Int'];
};

export type UserRatingFilterInput = {
  and?: InputMaybe<Array<UserRatingFilterInput>>;
  negative?: InputMaybe<ComparableInt32OperationFilterInput>;
  or?: InputMaybe<Array<UserRatingFilterInput>>;
  positive?: InputMaybe<ComparableInt32OperationFilterInput>;
  result?: InputMaybe<ComparableInt32OperationFilterInput>;
  total?: InputMaybe<ComparableInt32OperationFilterInput>;
  your?: InputMaybe<ComparableInt32OperationFilterInput>;
};

export type UserRatingSortInput = {
  negative?: InputMaybe<SortEnumType>;
  positive?: InputMaybe<SortEnumType>;
  result?: InputMaybe<SortEnumType>;
  total?: InputMaybe<SortEnumType>;
  your?: InputMaybe<SortEnumType>;
};

export enum UserRoleEnum {
  Admin = 'ADMIN',
  JrModerator = 'JR_MODERATOR',
  Moderator = 'MODERATOR',
  None = 'NONE',
  SiteDeveloper = 'SITE_DEVELOPER'
}

export type UserRoleEnumOperationFilterInput = {
  eq?: InputMaybe<UserRoleEnum>;
  in?: InputMaybe<Array<UserRoleEnum>>;
  neq?: InputMaybe<UserRoleEnum>;
  nin?: InputMaybe<Array<UserRoleEnum>>;
};

export type UserSortInput = {
  avatar?: InputMaybe<SortEnumType>;
  banner?: InputMaybe<SortEnumType>;
  banreportEndAt?: InputMaybe<SortEnumType>;
  createdAt?: InputMaybe<SortEnumType>;
  description?: InputMaybe<SortEnumType>;
  discordId?: InputMaybe<SortEnumType>;
  isBanned?: InputMaybe<SortEnumType>;
  lastOnline?: InputMaybe<SortEnumType>;
  level?: InputMaybe<SortEnumType>;
  money?: InputMaybe<SortEnumType>;
  nickname?: InputMaybe<SortEnumType>;
  permissions?: InputMaybe<SortEnumType>;
  phone?: InputMaybe<SortEnumType>;
  rating?: InputMaybe<UserRatingSortInput>;
  role?: InputMaybe<SortEnumType>;
  settings?: InputMaybe<SortEnumType>;
  sex?: InputMaybe<SortEnumType>;
  socialPoints?: InputMaybe<SortEnumType>;
  status?: InputMaybe<SortEnumType>;
  subscriptionEndAt?: InputMaybe<SortEnumType>;
  tag?: InputMaybe<SortEnumType>;
  totalFriends?: InputMaybe<SortEnumType>;
  totalSubscribers?: InputMaybe<SortEnumType>;
  trefs?: InputMaybe<SortEnumType>;
  updatedAt?: InputMaybe<SortEnumType>;
  userId?: InputMaybe<SortEnumType>;
  userRole?: InputMaybe<SortEnumType>;
  views?: InputMaybe<SortEnumType>;
  work?: InputMaybe<SortEnumType>;
};

export type UserStatus = {
  __typename?: 'UserStatus';
  heIsSubscriber: Scalars['Boolean'];
  isFriends: Scalars['Boolean'];
  youIsSubscriber: Scalars['Boolean'];
};

export enum UserTopEnum {
  Friends = 'FRIENDS',
  Rating = 'RATING',
  RatingN = 'RATING_N',
  SocialPoints = 'SOCIAL_POINTS',
  SocialPointsN = 'SOCIAL_POINTS_N',
  Subscribers = 'SUBSCRIBERS',
  Views = 'VIEWS',
  Years = 'YEARS'
}

/** A connection to a list of items. */
export type UsersConnection = {
  __typename?: 'UsersConnection';
  /** A list of edges. */
  edges?: Maybe<Array<UsersEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<User>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  totalCount: Scalars['Int'];
};

/** An edge in a connection. */
export type UsersEdge = {
  __typename?: 'UsersEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: User;
};

export type LoginMutationVariables = Exact<{ [key: string]: never; }>;


export type LoginMutation = { __typename?: 'Mutation', login: boolean };

export type GetMeQueryVariables = Exact<{ [key: string]: never; }>;


export type GetMeQuery = { __typename?: 'Query', me: { __typename?: 'User', userId: number, permissions: any, nickname: string, userRole: UserRoleEnum, avatar: string, settings: any, banreportEndAt?: any | null, subscriptionEndAt?: any | null } };

export type SiteOnlineQueryVariables = Exact<{
  type: OnlineTypes;
}>;


export type SiteOnlineQuery = { __typename?: 'Query', siteOnlineLogs: Array<{ __typename?: 'Online', max: number, avg: number, min: number, time: any }> };

export type ServerOnlineQueryVariables = Exact<{
  type: OnlineTypes;
}>;


export type ServerOnlineQuery = { __typename?: 'Query', serverOnlineLogs: Array<{ __typename?: 'Online', max: number, avg: number, min: number, time: any }> };

export type NewPlayersQueryVariables = Exact<{
  type: OnlineTypes;
}>;


export type NewPlayersQuery = { __typename?: 'Query', newPlayerLogs: Array<{ __typename?: 'NewPlayer', total: number, inc: number, time: any }> };

export type TopQueryVariables = Exact<{
  type: UserTopEnum;
}>;


export type TopQuery = { __typename?: 'Query', top: Array<{ __typename?: 'User', userId: number, discordId: any, socialPoints: number, nickname: string, level: number, views: number, totalFriends: number, totalSubscribers: number, userRole: UserRoleEnum, avatar: string, banner?: any | null, lastOnline: any, permissions: any, rating: { __typename?: 'UserRating', result: number } }> };

export type UserQueryVariables = Exact<{
  userId?: InputMaybe<Scalars['Int']>;
}>;


export type UserQuery = { __typename?: 'Query', users?: { __typename?: 'UsersConnection', nodes?: Array<{ __typename?: 'User', userId: number, socialPoints: number, discordId: any, nickname: string, userRole: UserRoleEnum, status: string, description: string, avatar: string, banner?: any | null, lastOnline: any, level: number, views: number, sex: number, settings: any, isBanned: boolean, permissions: any, subscriberUsers: Array<{ __typename?: 'Subscriber', subscriberId: number, subscriberNavigation: { __typename?: 'User', userId: number, discordId: any, nickname: string, userRole: UserRoleEnum, avatar: string, banner?: any | null, permissions: any, lastOnline: any, discordRoles: Array<{ __typename?: 'UserDiscordRole', id: any, position: number, name: string }> } }>, friendUsers: Array<{ __typename?: 'Friend', friendId: number, friendNavigation: { __typename?: 'User', userId: number, discordId: any, nickname: string, userRole: UserRoleEnum, avatar: string, banner?: any | null, permissions: any, lastOnline: any, discordRoles: Array<{ __typename?: 'UserDiscordRole', id: any, position: number, name: string }> } }>, rating: { __typename?: 'UserRating', result: number, total: number, positive: number, negative: number, your: number }, discordRoles: Array<{ __typename?: 'UserDiscordRole', id: any, position: number, name: string, color: string }> }> | null } | null };

export type StatusQueryVariables = Exact<{
  userId: Scalars['Int'];
}>;


export type StatusQuery = { __typename?: 'Query', status: { __typename?: 'UserStatus', heIsSubscriber: boolean, youIsSubscriber: boolean, isFriends: boolean } };

export type StartSubscribeMutationVariables = Exact<{
  userId: Scalars['Int'];
}>;


export type StartSubscribeMutation = { __typename?: 'Mutation', startSubscribe: { __typename?: 'User', userId: number } };

export type EndSubscribeMutationVariables = Exact<{
  userId: Scalars['Int'];
}>;


export type EndSubscribeMutation = { __typename?: 'Mutation', endSubscribe: { __typename?: 'User', userId: number } };

export type RemoveFriendMutationVariables = Exact<{
  userId: Scalars['Int'];
}>;


export type RemoveFriendMutation = { __typename?: 'Mutation', removeFriend: { __typename?: 'User', userId: number } };

export type SetBanMutationVariables = Exact<{
  userId: Scalars['Int'];
  isBanned?: InputMaybe<Scalars['Boolean']>;
}>;


export type SetBanMutation = { __typename?: 'Mutation', editUser: { __typename?: 'User', userId: number } };

export type SetRoleMutationVariables = Exact<{
  userId: Scalars['Int'];
  role?: InputMaybe<UserRoleEnum>;
}>;


export type SetRoleMutation = { __typename?: 'Mutation', editUser: { __typename?: 'User', userId: number } };

export type SaveUserMutationVariables = Exact<{
  userId: Scalars['Int'];
  avatar: Scalars['MediaLink'];
  banner: Scalars['MediaLink'];
  description: Scalars['String'];
  status: Scalars['String'];
  sex: Scalars['Int'];
}>;


export type SaveUserMutation = { __typename?: 'Mutation', editUser: { __typename?: 'User', userId: number } };

export type SetUserRoleMutationVariables = Exact<{
  userId: Scalars['Int'];
  userRole: UserRoleEnum;
}>;


export type SetUserRoleMutation = { __typename?: 'Mutation', editUser: { __typename?: 'User', userId: number } };

export type SetRatingMutationVariables = Exact<{
  userId: Scalars['Int'];
  isPositive?: InputMaybe<Scalars['Boolean']>;
}>;


export type SetRatingMutation = { __typename?: 'Mutation', setRating: { __typename?: 'User', userId: number } };

export type UsersQueryVariables = Exact<{
  search?: InputMaybe<Scalars['String']>;
  after?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  before?: InputMaybe<Scalars['String']>;
}>;


export type UsersQuery = { __typename?: 'Query', users?: { __typename?: 'UsersConnection', totalCount: number, nodes?: Array<{ __typename?: 'User', userId: number, discordId: any, nickname: string, userRole: UserRoleEnum, avatar: string, banner?: any | null, permissions: any, lastOnline: any, discordRoles: Array<{ __typename?: 'UserDiscordRole', id: any, position: number, name: string }> }> | null, pageInfo: { __typename?: 'PageInfo', hasPreviousPage: boolean, hasNextPage: boolean, endCursor?: string | null, startCursor?: string | null } } | null };

export type SetPermissionsMutationVariables = Exact<{
  userId: Scalars['Int'];
  permissions: Scalars['Long'];
}>;


export type SetPermissionsMutation = { __typename?: 'Mutation', editUser: { __typename?: 'User', permissions: any } };

export type AddSocialPointsMutationVariables = Exact<{
  userId: Scalars['Int'];
  socialPoints: Scalars['Int'];
}>;


export type AddSocialPointsMutation = { __typename?: 'Mutation', addSocialPoints: { __typename?: 'User', userId: number } };

export type SetSettingsMutationVariables = Exact<{
  userId: Scalars['Int'];
  r?: InputMaybe<Scalars['Boolean']>;
  rm?: InputMaybe<Scalars['Boolean']>;
  f?: InputMaybe<Scalars['Boolean']>;
  s?: InputMaybe<Scalars['Boolean']>;
}>;


export type SetSettingsMutation = { __typename?: 'Mutation', editUser: { __typename?: 'User', userId: number } };

export type IncViewMutationVariables = Exact<{
  userId: Scalars['Int'];
}>;


export type IncViewMutation = { __typename?: 'Mutation', incView: boolean };

export type RatingsQueryVariables = Exact<{ [key: string]: never; }>;


export type RatingsQuery = { __typename?: 'Query', me: { __typename?: 'User', userId: number, ratingTos: Array<{ __typename?: 'Rating', ratingId: number, positive: boolean, from: { __typename?: 'User', userId: number, discordId: any, nickname: string, userRole: UserRoleEnum, avatar: string, banner?: any | null, permissions: any, lastOnline: any, discordRoles: Array<{ __typename?: 'UserDiscordRole', id: any, position: number, name: string }> } }> } };


export const LoginDocument = gql`
    mutation login {
  login
}
    `;
export const GetMeDocument = gql`
    query getMe {
  me {
    userId
    permissions
    nickname
    userRole
    avatar
    settings
    banreportEndAt
    subscriptionEndAt
  }
}
    `;
export const SiteOnlineDocument = gql`
    query siteOnline($type: OnlineTypes!) {
  siteOnlineLogs(type: $type) {
    max
    avg
    min
    time
  }
}
    `;
export const ServerOnlineDocument = gql`
    query serverOnline($type: OnlineTypes!) {
  serverOnlineLogs(type: $type) {
    max
    avg
    min
    time
  }
}
    `;
export const NewPlayersDocument = gql`
    query newPlayers($type: OnlineTypes!) {
  newPlayerLogs(type: $type) {
    total
    inc
    time
  }
}
    `;
export const TopDocument = gql`
    query top($type: UserTopEnum!) {
  top(type: $type) {
    userId
    discordId
    socialPoints
    nickname
    level
    views
    totalFriends
    totalSubscribers
    rating {
      result
    }
    userRole
    avatar
    banner
    lastOnline
    permissions
  }
}
    `;
export const UserDocument = gql`
    query user($userId: Int) {
  users(first: 1, where: {userId: {eq: $userId}}) {
    nodes {
      userId
      socialPoints
      discordId
      nickname
      userRole
      status
      description
      avatar
      banner
      lastOnline
      level
      views
      sex
      settings
      isBanned
      permissions
      subscriberUsers {
        subscriberId
        subscriberNavigation {
          userId
          discordId
          nickname
          userRole
          avatar
          banner
          permissions
          lastOnline
          discordRoles {
            id
            position
            name
          }
        }
      }
      friendUsers {
        friendId
        friendNavigation {
          userId
          discordId
          nickname
          userRole
          avatar
          banner
          permissions
          lastOnline
          discordRoles {
            id
            position
            name
          }
        }
      }
      rating {
        result
        total
        positive
        negative
        your
      }
      discordRoles {
        id
        position
        name
        color
      }
    }
  }
}
    `;
export const StatusDocument = gql`
    query status($userId: Int!) {
  status(id: $userId) {
    heIsSubscriber
    youIsSubscriber
    isFriends
  }
}
    `;
export const StartSubscribeDocument = gql`
    mutation startSubscribe($userId: Int!) {
  startSubscribe(id: $userId) {
    userId
  }
}
    `;
export const EndSubscribeDocument = gql`
    mutation endSubscribe($userId: Int!) {
  endSubscribe(id: $userId) {
    userId
  }
}
    `;
export const RemoveFriendDocument = gql`
    mutation removeFriend($userId: Int!) {
  removeFriend(id: $userId) {
    userId
  }
}
    `;
export const SetBanDocument = gql`
    mutation setBan($userId: Int!, $isBanned: Boolean) {
  editUser(id: $userId, input: {isBanned: $isBanned}) {
    userId
  }
}
    `;
export const SetRoleDocument = gql`
    mutation setRole($userId: Int!, $role: UserRoleEnum) {
  editUser(id: $userId, input: {role: $role}) {
    userId
  }
}
    `;
export const SaveUserDocument = gql`
    mutation saveUser($userId: Int!, $avatar: MediaLink!, $banner: MediaLink!, $description: String!, $status: String!, $sex: Int!) {
  editUser(
    id: $userId
    input: {avatar: $avatar, banner: $banner, description: $description, status: $status, sex: $sex}
  ) {
    userId
  }
}
    `;
export const SetUserRoleDocument = gql`
    mutation setUserRole($userId: Int!, $userRole: UserRoleEnum!) {
  editUser(id: $userId, input: {role: $userRole}) {
    userId
  }
}
    `;
export const SetRatingDocument = gql`
    mutation setRating($userId: Int!, $isPositive: Boolean) {
  setRating(id: $userId, positive: $isPositive) {
    userId
  }
}
    `;
export const UsersDocument = gql`
    query users($search: String, $after: String, $first: Int, $last: Int, $before: String) {
  users(
    first: $first
    last: $last
    search: $search
    after: $after
    before: $before
    order: {lastOnline: DESC}
  ) {
    nodes {
      userId
      discordId
      nickname
      userRole
      avatar
      banner
      permissions
      lastOnline
      discordRoles {
        id
        position
        name
      }
    }
    totalCount
    pageInfo {
      hasPreviousPage
      hasNextPage
      endCursor
      startCursor
    }
  }
}
    `;
export const SetPermissionsDocument = gql`
    mutation setPermissions($userId: Int!, $permissions: Long!) {
  editUser(id: $userId, input: {permissions: $permissions}) {
    permissions
  }
}
    `;
export const AddSocialPointsDocument = gql`
    mutation addSocialPoints($userId: Int!, $socialPoints: Int!) {
  addSocialPoints(id: $userId, socialPoints: $socialPoints) {
    userId
  }
}
    `;
export const SetSettingsDocument = gql`
    mutation setSettings($userId: Int!, $r: Boolean, $rm: Boolean, $f: Boolean, $s: Boolean) {
  editUser(
    id: $userId
    input: {isNotifyOnReport: $r, isNotifyOnReportMessage: $rm, isNotifyOnNewFriend: $f, isNotifyOnNewSubscriber: $s}
  ) {
    userId
  }
}
    `;
export const IncViewDocument = gql`
    mutation incView($userId: Int!) {
  incView(id: $userId)
}
    `;
export const RatingsDocument = gql`
    query ratings {
  me {
    userId
    ratingTos {
      ratingId
      from {
        userId
        discordId
        nickname
        userRole
        avatar
        banner
        permissions
        lastOnline
        discordRoles {
          id
          position
          name
        }
      }
      positive
    }
  }
}
    `;
export type LoginMutationStore = OperationStore<LoginMutation, LoginMutationVariables>;
export type GetMeQueryStore = OperationStore<GetMeQuery, GetMeQueryVariables>;
export type SiteOnlineQueryStore = OperationStore<SiteOnlineQuery, SiteOnlineQueryVariables>;
export type ServerOnlineQueryStore = OperationStore<ServerOnlineQuery, ServerOnlineQueryVariables>;
export type NewPlayersQueryStore = OperationStore<NewPlayersQuery, NewPlayersQueryVariables>;
export type TopQueryStore = OperationStore<TopQuery, TopQueryVariables>;
export type UserQueryStore = OperationStore<UserQuery, UserQueryVariables>;
export type StatusQueryStore = OperationStore<StatusQuery, StatusQueryVariables>;
export type StartSubscribeMutationStore = OperationStore<StartSubscribeMutation, StartSubscribeMutationVariables>;
export type EndSubscribeMutationStore = OperationStore<EndSubscribeMutation, EndSubscribeMutationVariables>;
export type RemoveFriendMutationStore = OperationStore<RemoveFriendMutation, RemoveFriendMutationVariables>;
export type SetBanMutationStore = OperationStore<SetBanMutation, SetBanMutationVariables>;
export type SetRoleMutationStore = OperationStore<SetRoleMutation, SetRoleMutationVariables>;
export type SaveUserMutationStore = OperationStore<SaveUserMutation, SaveUserMutationVariables>;
export type SetUserRoleMutationStore = OperationStore<SetUserRoleMutation, SetUserRoleMutationVariables>;
export type SetRatingMutationStore = OperationStore<SetRatingMutation, SetRatingMutationVariables>;
export type UsersQueryStore = OperationStore<UsersQuery, UsersQueryVariables>;
export type SetPermissionsMutationStore = OperationStore<SetPermissionsMutation, SetPermissionsMutationVariables>;
export type AddSocialPointsMutationStore = OperationStore<AddSocialPointsMutation, AddSocialPointsMutationVariables>;
export type SetSettingsMutationStore = OperationStore<SetSettingsMutation, SetSettingsMutationVariables>;
export type IncViewMutationStore = OperationStore<IncViewMutation, IncViewMutationVariables>;
export type RatingsQueryStore = OperationStore<RatingsQuery, RatingsQueryVariables>;