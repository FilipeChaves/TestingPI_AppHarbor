## Controller
 * Utilização
  * Todos os métodos para visualização por HTML são precedidos do atributo (Anotação em java) HttpMethod(TipoDoPedido, StringURI);
  * Quando um utilizador, por exemplo, pede que lhe sejam mostrados todos os boards, é mostrada uma representação do estado actual dos boards, que neste caso é uma lista com todos os nomes dos boards existentes com um link para que possam ser mostradas todas as listas desse board;
  * Todos estes metodos retornam uma instancia de uma classe dentro do Namespace Trabalho_PI.Views;
  * Recebem toda a informação necessária para mostrar o ou os elementos pedidos pelo utilizador.