namespace RecyclerViewer
{
    // Photo: contains image resource ID and caption:
    public class Photo
    {
        // Photo ID for this photo:
        public int mPhotoID;

        // Caption text for this photo:
        public string mCaption;

        // Return the ID of the photo:
        public int PhotoID 
        { 
            get { return mPhotoID; } 
        }

        // Return the Caption of the photo:
        public string Caption 
        { 
            get { return mCaption; } 
        }
    }

    // Photo album: holds image resource IDs and caption:
    public class PhotoAlbum
    {
        // Built-in photo collection - this could be replaced with
        // a photo database:

        static Photo[] mBuiltInPhotos = {

};

        // Array of photos that make up the album:
        private Photo[] mPhotos;

        public PhotoAlbum ()
        {
            mPhotos = mBuiltInPhotos;
        }

        // Return the number of photos in the photo album:
        public int NumPhotos 
        { 
            get { return mPhotos.Length; } 
        }

        // Indexer (read only) for accessing a photo:
        public Photo this[int i] 
        {
            get { return mPhotos[i]; }
        }
    }
}
