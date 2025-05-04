using Anthropic.SDK.Common;

namespace PolishTaxer.Functions
{
    internal sealed class YearlyTaxRulesProvider
    {
        [Function("This function returns the tax rules per year")]
        public static async Task<string> GetTaxRulesAsync([FunctionParameter("Year for tax rules", true)] int year)
        {
            if (year < 2023)
            {
                return """
                        {
                            "calculation": "base income reduced by social tax and sum of costs then muliplied by PIT rate",
                            "taxes": [
                                {
                                    "name": "PIT",
                                    "rate": 0.12
                                },
                                {
                                    "name": "social",
                                    "static": 1000
                                }
                            ],
                            "rules": [
                                "car used for private reasons can only deduce 50% of VAT that can be added as costs",
                                "car used for private reasons can only deduct 100% of costs",
                            ]             
                        }
                    """;
            }

            return """
                        {
                            "calculation": "base income reduced by social tax and sum of costs then muliplied by PIT rate",
                            "taxes": [
                                {
                                    "name": "PIT",
                                    "rate": 0.15
                                },
                                {
                                    "name": "social",
                                    "static": 1500
                                }
                            ],
                            "rules": [
                                "car used for private reasons can only deduce 50% of VAT that can be added as costs",
                                "car used for private reasons can only deduct 75% of costs",
                            ]             
                        }
                    """;
        }
    }
}
