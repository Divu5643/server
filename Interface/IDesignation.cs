using perfomanceSystemServer.Models;

namespace perfomanceSystemServer.Interface
{
    public interface IDesignation
    {
        public List<DesignationMaster> getDesignations();
        public Boolean addDesignation(string designation);
        public Boolean DeleteDesignation(int designationId);
    }
}