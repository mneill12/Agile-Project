using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ServiceModel;
using CSC3045.Agile.Client.Contracts;
using CSC3045.Agile.Client.Entities;

namespace CSC3045.Agile.Client.Proxies
{
    [Export(typeof (IProjectService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectClient : ClientBase<IProjectService>, IProjectService
    {
        public IEnumerable<Project> GetProjectsForProjectManager(int projectManagerId)
        {
            return Channel.GetProjectsForProjectManager(projectManagerId);
        }

        public IEnumerable<Project> GetProjectsForProductOwner(int productOwnerId)
        {
            return Channel.GetProjectsForProductOwner(productOwnerId);
        }

        public IEnumerable<Project> GetProjectsForAccount(int accountId)
        {
            return Channel.GetProjectsForAccount(accountId);
        }
        public Project CreateProject(Project project)
        {
            return Channel.CreateProject(project);
        }

        public void UpdateProjectInfo(Project project)
        {
            Channel.UpdateProjectInfo(project);
        }

        public Project GetProjectInfo(int projectId)
        {
            return Channel.GetProjectInfo(projectId);
        }
    }
}