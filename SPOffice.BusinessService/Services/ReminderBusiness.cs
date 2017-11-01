using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class ReminderBusiness : IReminderBusiness
    {
        private IReminderRepository _reminderRepository;

        public ReminderBusiness(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }
        public List<Reminder> GetAllReminders()
        {
            return _reminderRepository.GetAllReminders();
        }
    }
}