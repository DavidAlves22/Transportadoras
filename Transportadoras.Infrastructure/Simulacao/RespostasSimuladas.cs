namespace Transportadoras.Infrastructure.Simulacao;

public static class RespostasSimuladas
{
    public static readonly IReadOnlyDictionary<string, string> Loggi = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["LG123456789"] = """
            {
                "trackingCode": "LG123456789",
                "events": [
                { "status": "posted", "timestamp": "2026-07-10T08:00:00Z", "description": "Objeto postado", "city": "São Paulo/SP" },
                { "status": "in_transit", "timestamp": "2026-07-11T14:30:00Z", "description": "Em trânsito", "city": "Campinas/SP" },
                { "status": "out_for_delivery", "timestamp": "2026-07-12T09:15:00Z", "description": "Saiu para entrega", "city": "Campinas/SP" },
                { "status": "delivered", "timestamp": "2026-07-12T16:40:00Z", "description": "Entregue ao destinatário", "city": "Campinas/SP" }
                ]
            }
            """,

        ["LG555000111"] = """
            {
                "trackingCode": "LG555000111",
                "events": [
                { "status": "posted", "timestamp": "2026-07-14T10:20:00Z", "description": "Objeto postado", "city": "Curitiba/PR" },
                { "status": "in_transit", "timestamp": "2026-07-15T07:05:00Z", "description": "Em trânsito", "city": "Joinville/SC" }
                ]
            }
            """
    };

    public static readonly IReadOnlyDictionary<string, string> TotalExpress = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["TE987654321"] = """
            {
                "codigoObjeto": "TE987654321",
                "ocorrencias": [
                { "codStatus": 1, "situacao": "POSTADO", "dataHora": "10/07/2026 09:30", "praca": "RIO DE JANEIRO/RJ" },
                { "codStatus": 2, "situacao": "EM TRANSITO", "dataHora": "11/07/2026 18:45", "praca": "BELO HORIZONTE/MG" },
                { "codStatus": 6, "situacao": "ENTREGUE", "dataHora": "12/07/2026 16:40", "praca": "CAMPINAS/SP" }
                ]
            }
            """,

        ["TE111222333"] = """
            {
                "codigoObjeto": "TE111222333",
                "ocorrencias": [
                { "codStatus": 1, "situacao": "POSTADO", "dataHora": "15/07/2026 11:00", "praca": "SALVADOR/BA" },
                { "codStatus": 3, "situacao": "SAIU PARA ENTREGA", "dataHora": "16/07/2026 08:10", "praca": "SALVADOR/BA" }
                ]
            }
            """
    };
}
