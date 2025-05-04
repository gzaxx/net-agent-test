using Anthropic.SDK.Common;

namespace PolishTaxer.Functions;

internal class YearlyLedgerProvider
{
    [Function("This function returns the ledger per year")]
    public static async Task<string> GetLedgerAsync([FunctionParameter("Year for ledger", true)] int year)
    {
        if (year < 2023)
        {
            return """
                    {
                        "ledger": [
                            {
                                "type": "income",
                                "net": 10000,
                                "tax": 2300
                            },
                            {
                                "type": "costs",
                                "descirption": "car leasing rate",
                                "amount": 2000
                            }
                        ]
                    }
                """;
        }

        return """
                    {
                        "declaration": "car is used privately",
                        "ledger": [
                            {
                                "type": "income",
                                "net": 10000,
                                "tax": 2300
                            },
                            {
                                "type": "costs",
                                "descirption": "car leasing rate",
                                "net": 2000,
                                "tax": 460
                            }
                        ]
                    }
                """;
    }
}
