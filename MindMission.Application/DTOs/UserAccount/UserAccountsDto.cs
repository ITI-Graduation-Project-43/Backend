using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.DTOs.UserAccount
{
    public class UserAccountsDto
    {
        public string UserId { get; set; }
        public List<UpdatedUserAccount> userAccounts { get; set; }
    }


    public class UpdatedUserAccount
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string accountName { get; set; }
        public string accountDomain { get; set; }
    }
}
