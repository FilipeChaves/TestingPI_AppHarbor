## Utils
 * LoginUtils - Ferramentas úteis relacionadas com o login das contas
  * CreateSalt - Cria o salt random que é guardado no modelo de uma conta
  * CreatePasswordHash - Combina o hashcode da password com o salt
  * EncryptPassword - Utiliza as duas funções acima descritas
  * AddToConfirmList - Adiciona um utilizador à lista de confirmação de conta
  * GenerateConfirmationCode - Gera um código de confirmação
  * RegisterAccount - Regista a conta na base de dados
  * ComparePasswords - Compara duas passwords
  * StrToByteArray - Converte uma String para um array de bytes
  * SendConfirmationMail - Envia o mail de confirmação para o email do utilizador