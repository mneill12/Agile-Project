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
        public Project AddProject(Project project)
        {
            return Channel.AddProject(project);
        }

        public Project GetProjectInfo(int projectId)
        {
            return Channel.GetProjectInfo(projectId);
        }

        public IEnumerable<Project> GetProjectsByProjectManager(int projectManagerId)
        {
            return Channel.GetProjectsByProjectManager(projectManagerId);
        }

        public IEnumerable<Project> GetProjectsByProductOwner(int productOwnerId)
        {
            return Channel.GetProjectsByProductOwner(productOwnerId);
        }

        public void UpdateProjectInfo(Project project)
        {
            Channel.UpdateProjectInfo(project);
        }

        public IEnumerable<Project> GetProjectsByAccount(int accountId)
        {
            return Channel.GetProjectsByAccount(accountId);
        }
    }
}