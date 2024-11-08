using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;

namespace perfomanceSystemServer.Services
{
    public class DesignationService : IDesignation
    {
        private readonly AppDbContext? _appDbContext;

        public DesignationService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<DesignationMaster> getDesignations()
        {

            List<DesignationMaster> desgList = _appDbContext!.DesignationMasters.Where(obj=> !obj.IsDeleted).ToList();
            return desgList;

        }
        public Boolean addDesignation(string designation) {

            DesignationMaster newDesignation = new DesignationMaster()
            {
                Designation = designation
            };
            _appDbContext!.DesignationMasters.Add(newDesignation);
            _appDbContext.SaveChanges();
            return true;
        }
        public Boolean DeleteDesignation(int designationId) {
            DesignationMaster? deletedDesignation = _appDbContext!.DesignationMasters.Find(designationId);
            if (deletedDesignation == null) {
                return false;
            }
            deletedDesignation.IsDeleted = true;
            _appDbContext.Update(deletedDesignation);
            _appDbContext.SaveChanges();
            return true;
        }
    }
}
