using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFTHelper.Models.TarkovTools;
using EFTHelper.Services;

namespace EFTHelper.Helpers
{
    public class EFTTasksHelper
    {
        #region Fields

        private readonly TarkovToolsService _tarkovToolsService;
        private List<EFTTaskBase> _EFTTasks;

        #endregion

        public EFTTasksHelper(TarkovToolsService tarkovToolsService)
        {
            _tarkovToolsService = tarkovToolsService;
        }

        public async Task LoadEFTTasks()
        {
            var response = await _tarkovToolsService.GetEFTTasks();
            _EFTTasks = response.Quests;
        }

        public async Task<EFTTaskBase> GetTask(string taskId)
        {
            if (_EFTTasks == null || !_EFTTasks.Any())
            {
                await LoadEFTTasks();
            }

            return _EFTTasks.FirstOrDefault(x => x.Id.Equals(taskId, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
