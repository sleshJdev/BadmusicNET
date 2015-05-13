using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MvcSimpleMusicSite_CourseProject.Resources.Models;
using MvcSimpleMusicSite_CourseProject.Helpers;

namespace MvcSimpleMusicSite_CourseProject.Models
{
        ///// <summary>
        ///// Общее число прослушиваний
        ///// </summary>
        //[LocalizedDisplayName("TotalPlays", NameResourceType = typeof(NamesAtrRes))]
        //public int TotalPlaysCount { get; set; }

        ///// <summary>
        ///// Прослушиваний за период
        ///// </summary>
        //[LocalizedDisplayName("PeriodPlays", NameResourceType = typeof(NamesAtrRes))]
        //public int PeridPlaysCount { get; set; }

    public class Track
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [LocalizedDisplayName("Number", NameResourceType = typeof(NamesAtrRes))]
        public int Id { get; set; }

        /// <summary>
        /// Название композиции
        /// </summary>
        [MaxLength(50)] 
        [LocalizedDisplayName("TrackTitle", NameResourceType = typeof(NamesAtrRes))]
        public string Title { get; set; }

        /// <summary>
        /// Путь к файлу
        /// </summary>
        [LocalizedDisplayName("PathToFile", NameResourceType = typeof(NamesAtrRes))]
        public string Mp3Url { get; set; }

        /// <summary>
        /// Дата добавления
        /// </summary>       
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)] 
        [LocalizedDisplayName("Added", NameResourceType = typeof(NamesAtrRes))]
        [DataType(DataType.DateTime)]       
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// Время
        /// </summary>
        [MaxLength(50)]
        [LocalizedDisplayName("Time", NameResourceType = typeof(NamesAtrRes))]
        public string Time { get; set; }


        /// <summary>
        /// Размер файла
        /// </summary>
        [MaxLength(50)]
        [LocalizedDisplayName("Size", NameResourceType = typeof(NamesAtrRes))]
        public string Size { get; set; }

        /// <summary>
        /// Скорость потока
        /// </summary>
        [MaxLength(50)]
        [LocalizedDisplayName("Bitrate", NameResourceType = typeof(NamesAtrRes))]
        public string Bitrate { get; set; }       

        /// <summary>
        /// Жанр композиции
        /// </summary>
        [LocalizedDisplayName("GenreTitle", NameResourceType = typeof(NamesAtrRes))]
        public int GenreId { get; set; }

        /// <summary>
        /// Жанр. навигационное свойство
        /// </summary>         
        public Genre Genre { get; set; }

        /// <summary>
        /// Название альбома
        /// </summary>
        [LocalizedDisplayName("AlbumTitle", NameResourceType = typeof(NamesAtrRes))]
        public int AlbumId { get; set; }

        /// <summary>
        /// Название альбома. Навигационное свойство
        /// </summary>        
        public Album Album { get; set; }

        /// <summary>
        /// Лайки. 
        /// </summary>
        [LocalizedDisplayName("Likes", NameResourceType = typeof(NamesAtrRes))]
        public ICollection<Like> Likes { get; set; }

        /// <summary>
        /// Скачиваний
        /// </summary>
        [LocalizedDisplayName("Dowloads", NameResourceType = typeof(NamesAtrRes))]
        public ICollection<Dowload> Dowloads { get; set; }

        /// <summary>
        /// Прослушиваний
        /// </summary>
        [LocalizedDisplayName("Listened", NameResourceType = typeof(NamesAtrRes))]
        public ICollection<Listened> Listened { get; set; }
    }

    public class Genre
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [LocalizedDisplayName("Number", NameResourceType = typeof(NamesAtrRes))]
        public int Id { get; set; }

        /// <summary>
        /// Имя жанра
        /// </summary>
        [MaxLength(50)] 
        [LocalizedDisplayName("DescriptionGenre", NameResourceType = typeof(NamesAtrRes))]
        public string Title { get; set; }

        /// <summary>
        /// Описание жанра
        /// </summary>         
        [LocalizedDisplayName("TrackOfTheGenre", NameResourceType = typeof(NamesAtrRes))]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// Треки данного жанра. Навигационное свойство
        /// </summary>
        [LocalizedDisplayName("TrackOfTheGenre", NameResourceType = typeof(NamesAtrRes))]
        public ICollection<Track> Tracks { get; set; }

    }

    public class Artist
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [LocalizedDisplayName("Number", NameResourceType = typeof(NamesAtrRes))]
        public int Id { get; set; }

        /// <summary>
        /// Имя исполнителя
        /// </summary>
        [MaxLength(50)]
        [LocalizedDisplayName("ArtistTitle", NameResourceType = typeof(NamesAtrRes))]
        public string Title { get; set; }

        /// <summary>
        /// Путь к фотографии
        /// </summary>
        [LocalizedDisplayName("AlbumPhoto", NameResourceType = typeof(NamesAtrRes))]
        public string PhotoUrl { get; set; }


        /// <summary>
        /// Альбомы исполнителя. Навигационное свойство
        /// </summary>
        [LocalizedDisplayName("AlbumsOfTheArtist", NameResourceType = typeof(NamesAtrRes))]
        public ICollection<Album> Albums { get; set; }
    }

    public class Album
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [LocalizedDisplayName("Number", NameResourceType = typeof(NamesAtrRes))]
        public int Id { get; set; }

        /// <summary>
        /// Название альбома
        /// </summary>
        [MaxLength(50)]
        [LocalizedDisplayName("AlbumTitle", NameResourceType = typeof(NamesAtrRes))]
        public string Title { get; set; }

        /// <summary>
        /// Путь к фотографии
        /// </summary>
        [LocalizedDisplayName("AlbumPhoto", NameResourceType = typeof(NamesAtrRes))]
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        [MaxLength(50)]
        [LocalizedDisplayName("AlbumYear", NameResourceType = typeof(NamesAtrRes))]
        public string Year { get; set; }

        /// <summary>
        /// Исполнитель композиции
        /// </summary>
        [LocalizedDisplayName("ArtistTile", NameResourceType = typeof(NamesAtrRes))]
        public int ArtistId { get; set; }

        /// <summary>
        /// Артист. Навигационное свойство
        /// </summary>       
        public Artist Artist { get; set; }    

        /// <summary>
        /// Треки альбома. Навигационное свойство
        /// </summary>        
        public ICollection<Track> Tracks { get; set; }
    }

    public class Like
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [LocalizedDisplayName("Number", NameResourceType = typeof(NamesAtrRes))]
        public int Id { get; set; }

        /// <summary>
        /// Пользователь, поставивший лайк
        /// </summary>
        [LocalizedDisplayName("User", NameResourceType = typeof(NamesAtrRes))]
        public string UserName { get; set; }

        /// <summary>
        /// Трек, которому поставил лайк
        /// </summary>
        [LocalizedDisplayName("UserPutLikesTracks", NameResourceType = typeof(NamesAtrRes))]
        public int TrackId { get; set; }

        /// <summary>
        /// Трек, которому поставил лайк. Навигационное свойство
        /// </summary>         
        public Track Track { get; set; }
    }

    public class Listened
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [LocalizedDisplayName("Number", NameResourceType = typeof(NamesAtrRes))]
        public int Id { get; set; }

        /// <summary>
        /// Пользователь, который прослушайл трек
        /// </summary>
        [LocalizedDisplayName("User", NameResourceType = typeof(NamesAtrRes))]
        public string UserName { get; set; }

        /// <summary>
        /// Трек, который он прослушал
        /// </summary>
        [LocalizedDisplayName("UserListenedTracks", NameResourceType = typeof(NamesAtrRes))]
        public int TrackId { get; set; }

        /// <summary>
        /// Трек, который он прослушал. Навигационное свойство
        /// </summary>         
        public Track Track { get; set; }
    }

    public class Dowload
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [LocalizedDisplayName("Number", NameResourceType = typeof(NamesAtrRes))]
        public int Id { get; set; }

        /// <summary>
        /// Пользователь, который скачал трек
        /// </summary>
        [LocalizedDisplayName("User", NameResourceType = typeof(NamesAtrRes))]
        public string UserName { get; set; }

        /// <summary>
        /// Трек, который он скачал
        /// </summary>
        [LocalizedDisplayName("UserDowloadsTracks", NameResourceType = typeof(NamesAtrRes))]
        public int TrackId { get; set; }

        /// <summary>
        /// Трек, кторый он скачал
        /// </summary>        
        public Track Track { get; set; }
    }
}