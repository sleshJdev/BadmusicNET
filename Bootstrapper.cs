using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using MvcSimpleMusicSite_CourseProject.Models;
using System.Data.Entity;

namespace MvcSimpleMusicSite_CourseProject
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<MusicContext>(new HierarchicalLifetimeManager());

            container.RegisterType<ITrackRepository, TrackRepository>();

            container.RegisterType<IArtistRepository, ArtistRepository>();

            container.RegisterType<IGenreRepository, GenreRepository>();

            container.RegisterType<IAlbumRepository, AlbumRepository>();

            container.RegisterType<ILikeRepository, LikeRepository>();

            container.RegisterType<IListenedRepository, ListenedRepository>();

            container.RegisterType<IDowloadRepository, DowloadRepository>();            

            return container;
        }         
    }
}