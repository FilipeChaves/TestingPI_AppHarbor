using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Trabalho_PI.Elements;
using WebGarten2.Html;
using System.Threading.Tasks;

namespace Trabalho_PI.Views
{
    class BoardsView: HtmlDoc
    {
        public BoardsView(IEnumerable<Board> boards)
            :base("Boards",
                H1(Text("Boards available:")),
                Ul(
                    boards.Select(board => Li(A(ResolveUri.ForBoard(board), board.name))).ToArray()
                    ),
                H2(Text("Create a new board:")),
                Form("POST", ResolveUri.ForBoards(),
                    Label("name","Name: "),InputText("name"),
                    Label("descr","Description: "),InputText("descr"),
                    InputSubmit("Submit"))
                ){}
    }
    
    class ListsView : HtmlDoc
    {
        public ListsView(IEnumerable<List> lists, Board board)
            : base("Lists from board " + board.name,
                H1(Text("Lists from board \"" + board.name + "\":")),
                Ul(
                    lists.Select(list => Li(A(ResolveUri.ForList(list), list.name))).ToArray()
                    ),
                H2(Text("Create a new list:")),
                Form("POST", ResolveUri.ForBoard(board),
                    Label("name", "Name: "), InputText("name")),
                    Form("GET", ResolveUri.ForBoards(), InputSubmit("Back"))
                ) { }
    }

    class CardsView : HtmlDoc
    {
        /*Neste momento o "cards" tem todas as listas nos boards todos, depois vai deixar de ter!*/
        public CardsView(IEnumerable<Card> cards, List list)
            : base("Cards from list " + list.name,
                   H1(Text("Cards from list \"" + list.name + "\":")),
                   Ul(
                       cards.Select( card => Li(A(ResolveUri.ForCard(card), card.name))).ToArray()
                       ),
                   H3(Text("Create a new card:")),
                   Form("POST", ResolveUri.ForList(list),
                        Label("name", "Name: "), InputText("name"),
                        Label("descr", "Description: "), InputText("descr"),
                        Label("initialDate", "Initial Date: "), HtmlUtils.InputDate("date"),
                        Label("dueDate", "Due Date: "), HtmlUtils.InputDate("dueDate"),
                        Label("idx", "Index"), InputText("idx"),
                        InputSubmit("Submit")
                    ),
                    Form("POST", ResolveUri.ForListToRemove(list), Label("remove", "If this list is empty, you can remove it: "), InputSubmit("Remove")),
                    Form("GET", ResolveUri.ForBoard(list.board), InputSubmit("Back"))
                ) { }
    }

    class CardView : HtmlDoc
    {
        /*Neste momento o "cards" tem todas as listas nos boards todos, depois vai deixar de ter!*/
        public CardView(Card card)
            : base("Card " + card.name,
                   H1(Text("Card \"" + card.name + "\":")),
                   Ul(
                    Li(Text("Description: " + card.description)),
                    Li(Text("Creation date: " + card.creationDate))
                   ),
                    H3(Text("Move card:")),
                   Form("POST", ResolveUri.ForCard(card),
                        Label("kix", "Move card this card to position: "), InputText("kix"),
                        Label("lid", "And to List"), InputText("lid"),
                        InputSubmit("Submit")
                    ),
                    Form("POST", ResolveUri.ForCardWithoutList(card),
                        Label("archive", "Move card to archive"),
                        InputSubmit("Submit")),
                    Form("GET", ResolveUri.ForList(card.list), InputSubmit("Back"))
                ) { }
    }

    class CardArchiveView : HtmlDoc
    {
        public CardArchiveView(Card card)
            : base("Card " + card.name,
                   H1(Text("Card \"" + card.name + "\":")),
                   Ul(
                    Li(Text("Description: " + card.description)),
                    Li(Text("Creation date: " + card.creationDate))
                   ),
                    card.list == null ? Text("") : Form("POST", ResolveUri.ForCardWithoutList(card),
                        H3(Text("Move card:")),
                        Label("archive", "Move card to archive"),
                        InputSubmit("Submit")),
                    Form("GET", ResolveUri.ForBoards(), InputSubmit("All Boards"))
                ) { }
    }

    public class HtmlUtils : HtmlBase
    {
        public static IWritable InputDate(String name)
        {
            return new HtmlElem("input")
                .WithAttr("type", "date")
                .WithAttr("name", name);
        }
    }
}
