## Controllers
 * AccountController - Controladores de acções das contas de utilizador
  * LogOn - Acessível por qualquer utilizador, permite efectuar o login de uma conta
  * LogOut - Permite efectuar o logout de uma conta
  * Register - Acessível por qualquer utilizador, permite registar uma nova conta
  * Confirmation - Exibe apenas uma vista de confirmação do envio do mail de interacção com o código de verificação do email inserido no registo
  * Verify - Recebe o código de verificação e verifica se o mesmo está associado a alguma conta por verificar
  * Settings - Exibe uma vista com possibilidade se seleccionar a opção de mudar a password ou o email
  * ChangePassword - Permite alterar a password do utilizador
  * ChangeEmail - Permite alterar o email do utilizador
  * Delete - Apaga da base de dados o utilizador passado como argumento
  * SelfDelete - Efectua logout e utiliza o método acima, passado como argumento o próprio
  * SetImage - Permite associar uma imagem a uma conta
  * ShowInfo - Apresenta a informação de uma conta (imagem, nick, email e acesso)

 * AdminController - Controladores de acções das contas de utilizador com acesso "Admin"
  * ControlPanel - Apresenta uma tabela com todos os utilizadores, podendo eliminar ou promover/despromover um utilizador
  * ToUser - Despromove um utilizador que é administrador
  * ToAdmin - Promove um utilizador a administrador

 * HomeController - Apresenta a Home Page
  * Index - Única acção deste controlador, permite que o utilizador aceda aos seus quadros ou efectue operações relacionadas com a sua conta