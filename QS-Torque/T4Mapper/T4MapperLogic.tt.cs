//<#@ template debug="false" hostspecific="true" language="C#" #>
//<#@ assembly name="System.Core" #>
//<#@ import namespace="System.Linq" #>
//<#@ import namespace="System.Text" #>
//<#@ import namespace="System.Collections.Generic" #>
//<#@ output extension=".cs" #>

//<#+
#if NOT_IN_T4
namespace T4Mapper
{
    using System.Collections.Generic;
#endif
    public class SyntaxTreeEntry
    {
        public string action;
        public List<SyntaxTreeEntry> subnodes;
    }

    public class MappigDefinitionParser
    {
        private class IdentifierResult
        {
            public string leftovers;
            public bool parsingSuccess;
            public string identifier;
        }

        private class SyntaxTreeEntryResult
        {
            public string leftovers;
            public bool parsingSuccess;
            public SyntaxTreeEntry result;
        }

        private class NextCharacterResult
        {
            public string leftovers;
            public bool parsingSuccess;
        }

        private IdentifierResult ParseIdentifier(string sourceCode)
        {
            string token = "";
            var processedTokens = new List<string>();
            for (int i = 0; i <= sourceCode.Length; ++i)
            {
                if (i == sourceCode.Length || char.IsWhiteSpace(sourceCode[i]) || sourceCode[i] == '(' || sourceCode[i] == ')')
                {
                    if (token.Length != 0)
                    {
                        return new IdentifierResult
                        {
                            leftovers = sourceCode.Substring(i),
                            parsingSuccess = true,
                            identifier = token
                        };
                    }
                }
                else
                {
                    token += sourceCode[i];
                }
            }

            return new IdentifierResult
            {
                leftovers = sourceCode,
                identifier = "",
                parsingSuccess = false
            };
        }

        private NextCharacterResult ParseNextCharacter(string sourceCode, char character)
        {
            for (int i = 0; i < sourceCode.Length; ++i)
            {
                if (!char.IsWhiteSpace(sourceCode[i]) && sourceCode[i] == character)
                {
                    return new NextCharacterResult
                    {
                        parsingSuccess = true,
                        leftovers = sourceCode.Substring(i + 1)
                    };
                }
                else if (!char.IsWhiteSpace(sourceCode[i]))
                {
                    return new NextCharacterResult
                    {
                        parsingSuccess = false,
                        leftovers = sourceCode
                    };
                }
            }
            return new NextCharacterResult
            {
                parsingSuccess = false,
                leftovers = sourceCode
            };
        }

        private SyntaxTreeEntryResult ParseSyntaxTreeEntry(string sourceCode)
        {
            var withSubnodes = false;
            var openParenResult = ParseNextCharacter(sourceCode, '(');
            if (openParenResult.parsingSuccess)
            {
                withSubnodes = true;
            }

            IdentifierResult actionResult = ParseIdentifier(openParenResult.leftovers);
            if (!actionResult.parsingSuccess)
            {
                return new SyntaxTreeEntryResult
                {
                    leftovers = sourceCode,
                    parsingSuccess = false,
                    result = null
                };
            }

            var result = new SyntaxTreeEntry
            {
                action = actionResult.identifier,
                subnodes = new List<SyntaxTreeEntry>()
            };

            var leftovers = actionResult.leftovers;
            if (withSubnodes)
            {
                SyntaxTreeEntryResult subnodeActionResult;
                while (true)
                {
                    var closeParen = ParseNextCharacter(leftovers, ')');
                    leftovers = closeParen.leftovers;
                    if (closeParen.parsingSuccess)
                    {
                        break;
                    }

                    subnodeActionResult = ParseSyntaxTreeEntry(leftovers);
                    leftovers = subnodeActionResult.leftovers;
                    if (!subnodeActionResult.parsingSuccess)
                    {
                        break;
                    }
                    result.subnodes.Add(subnodeActionResult.result);
                }
            }

            return new SyntaxTreeEntryResult
            {
                leftovers = leftovers,
                parsingSuccess = true,
                result = result
            };
        }

        public SyntaxTreeEntry Parse(string sourceCode)
        {
            var result = ParseSyntaxTreeEntry(sourceCode);
            return result.result;
        }
    }

#if NOT_IN_T4
}
#endif
//#>
