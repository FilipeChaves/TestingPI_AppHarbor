## Modelos
 * Entidades concretas:
  * AccountModel - Modelo de um conta de utilizador.
  * ListsModel - Modelo de uma lista. 
  * BoardsModel - Modelo de um board.
  * CardsModel - Modelo de um card.

 * Entidades que apenas são utilizadas numa view de modo a ver mais facil a utilização dos dados inseridos por um utilizador:
  * GiveRightsModel - Modelo para dar acesso a um utilizador de um board. Dependendo de onde é chamado pode ser para dar direito de leitura ou de escrita.
  * ChangePasswordModel - Modelo que tem o proposito de o utilizador colocar numa view uma password antiga e duas novas iguais.
  * ChangePasswordModel - Modelo que tem o proposito de o utilizador colocar numa view um email antigo e um novo.
  * MoveToListModel - Modelo que tem o proposito de o utilizador mover um cartão para uma lista diferente.
  * RegisterAccountModel - Modelo usado para que um utilizador se registe.