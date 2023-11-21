namespace ProdutosApp.Validations
{
    public static class Validacoes
    {
        public static string ValidarDescricao(this string descricao) {
            descricao = descricao.Trim();

            if (string.IsNullOrWhiteSpace(descricao))
            {
                return "O título é obrigatório.";
            }

            if (descricao.Length < 3 || descricao.Length > 255) {
                return "A descrição deve ter entre 3 e 255 caracteres.";
            }

            return string.Empty;
        }

        public static string ValidarUrlImagem(this string urlImagem) {
            urlImagem = urlImagem.Trim();

            if (string.IsNullOrWhiteSpace(urlImagem))
            {
                return "A URL da imagem é obrigatória.";
            }

            if (urlImagem.Length < 8 )
            {
                return "A Url da imagem tem que ter mais de 7 caracteres.";
            }

            return string.Empty;
        }

        public static string ValidarQuantidade(this int quantidade) {
            if (quantidade < 1 || quantidade > 5)
            {
                return "A quantidade deve ser entre 1 e 5 produtos.";
            }

            return string.Empty;
        }

        public static string ValidarCodigoEAN(this string ean)
        {
            ean = ean.Trim().Replace("-", "");

            if (string.IsNullOrWhiteSpace(ean))
            {
                return "O EAN é obrigatório.";
            }

            if (ean.Length == 13)
            {
                if(!IsValidEAN(ean)) { 
                    return "O EAN não é válidol.";
                } else
                {
                    return string.Empty;
                }
            }
            
            return "O EAN não segue o padrão internacional.";
        }

        private static bool IsValidEAN(string codigoEAN)
        {
            if (!codigoEAN.All(char.IsDigit))
            {
                return false;
            }

            const string checkSum = "131313131313";

            int digito = int.Parse(codigoEAN[codigoEAN.Length - 1].ToString());
            if (digito == 0)
            {
                digito = 10;
            }
            string ean = codigoEAN.Substring(0, codigoEAN.Length - 1);

            int sum = 0;
            for (int i = 0; i <= ean.Length - 1; i++)
            {
                sum += int.Parse(ean[i].ToString()) * int.Parse(checkSum[i].ToString());
            }
            int calculo = 10 - (sum % 10);
            return (digito == calculo);

        }

        public static string ValidarPreco(this double preco)
        {
            if (preco <= 0)
            {
                return "O preço tem que ser superior a R$ 0,01.";
            }

            return string.Empty;
        }

    }
}