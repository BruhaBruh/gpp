using HotChocolate.Language;
using System.Text.RegularExpressions;

namespace GPPlanetGQL.GraphQL
{
    public class Scalars
    {
        public sealed class MediaLinkType : ScalarType<string, StringValueNode>
        {
            // we can inject services that have been registered
            // with the DI container
            public MediaLinkType()
                : base("MediaLink")
            {
                Description = "Прямая ссылка на медиафайл сервисов postimg/imgur/tenor. Поддерживаются jpeg/jpg/png/gif/webp";
            }

            private bool Validate(string v)
            {
                string pattern = "^((https:\\/\\/(i\\.postimg\\.cc|i\\.imgur\\.com|c\\.tenor\\.com)\\/.*(\\.jpeg|\\.jpg|\\.gif|\\.png|\\.webp))|(https:\\/\\/cdn\\.discordapp\\.com\\/avatars\\/.*)|)$";
                return Regex.IsMatch(v, pattern);
            }

            // is another StringValueNode an instance of this scalar
            protected override bool IsInstanceOfType(StringValueNode valueSyntax)
                => IsInstanceOfType(valueSyntax.Value);

            // is another string .NET type an instance of this scalar
            protected override bool IsInstanceOfType(string runtimeValue)
                => Validate(runtimeValue);

            public override IValueNode ParseResult(object? resultValue)
                => ParseValue(resultValue);

            // define how a value node is parsed to the string .NET type
            protected override string ParseLiteral(StringValueNode valueSyntax)
                => valueSyntax.Value;

            // define how the string .NET type is parsed to a value node
            protected override StringValueNode ParseValue(string runtimeValue)
                => new StringValueNode(runtimeValue);

            public override bool TryDeserialize(object? resultValue,
                out object? runtimeValue)
            {
                runtimeValue = null;

                if (resultValue is string s && Validate(s))
                {
                    runtimeValue = s;
                    return true;
                }

                return false;
            }

            public override bool TrySerialize(object? runtimeValue,
                out object? resultValue)
            {
                resultValue = null;

                if (runtimeValue is string s && Validate(s))
                {
                    resultValue = s;
                    return true;
                }

                return false;
            }
        }
    }
}
