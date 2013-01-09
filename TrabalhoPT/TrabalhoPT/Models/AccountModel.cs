using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoPT.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage = "Campo de preenchimento obrigatório")]
        public String Username { get; set; }
        public String Email { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        public String Salt { get; set; }
        public String ConfirmationCode { get; set; }
        public String ImageUrl { get; set; }
        public bool Confirmed { get; set; }
        public List<string> Roles = new List<string>() { "User" };
        private List<int> _canReadInBoards = new List<int>();
        private List<int> _canWriteInBoards = new List<int>();

        public void AddReadBoard(int id)
        {
            if(!_canReadInBoards.Contains(id))
                _canReadInBoards.Add(id);
        }

        public void AddWriteBoard(int id)
        {
            if (!_canWriteInBoards.Contains(id))
                _canWriteInBoards.Add(id);
        }

        public void AddBoard(BoardsModel bm)
        {
            AddReadBoard(bm.Id);
            AddWriteBoard(bm.Id);
        }

        public void RemoveReadBoard(int id)
        {
            if (_canReadInBoards.Contains(id))
                _canReadInBoards.Remove(id);
        }

        public void RemoveWriteBoard(int id)
        {
            if (_canWriteInBoards.Contains(id))
                _canWriteInBoards.Remove(id);
        }

        public void RemoveBoard(BoardsModel bm)
        {
            RemoveReadBoard(bm.Id);
            RemoveWriteBoard(bm.Id);
        }

        public bool CanReadBoard(int id)
        {
            return _canReadInBoards.Contains(id);
        }

        public bool CanWriteBoard(int id)
        {
            return _canWriteInBoards.Contains(id);
        }
    }
}