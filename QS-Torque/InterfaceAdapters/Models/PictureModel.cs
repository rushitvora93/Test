using Core.Entities;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;

namespace InterfaceAdapters.Models
{
    public class PictureModel : BindableBase, IQstEquality<PictureModel>, ICopy<PictureModel>
    {
        public Picture Entity { get; private set; }

        
        public long SeqId
        {
            get => Entity.SeqId;
            set
            {
                Entity.SeqId = value;
                RaisePropertyChanged();
            }
        }
        
        public int NodeId
        {
            get => Entity.NodeId;
            set
            {
                Entity.NodeId = value;
                RaisePropertyChanged();
            }
        }
        
        public long NodeSeqId
        {
            get => Entity.NodeSeqId;
            set
            {
                Entity.NodeSeqId = value;
                RaisePropertyChanged();
            }
        }
        
        public string FileName
        {
            get => Entity.FileName;
            set
            {
                Entity.FileName = value;
                RaisePropertyChanged();
            }
        }
        
        public ImageSource Image
        {
            get
            {
                if (Entity.ImageStream != null && Entity.ImageStream.Length != 0)
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    Entity.ImageStream.Position = 0;
                    bitmapImage.StreamSource = Entity.ImageStream;
                    bitmapImage.EndInit();
                    return bitmapImage;
                }
                else
                {
                    return null;
                }
            }
        }
        
        public long? FileType
        {
            get => Entity.FileType;
            set
            {
                Entity.FileType = value;
                RaisePropertyChanged();
            }
        }


        public PictureModel(Picture entity)
        {
            Entity = entity ?? new Picture();
            RaisePropertyChanged();
        }

        public static PictureModel GetModelFor(Picture entity)
        {
            return entity != null ? new PictureModel(entity) : null;
        }

        public bool EqualsById(PictureModel other)
        {
            return this.Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(PictureModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }

        public PictureModel CopyDeep()
        {
            return new PictureModel(Entity.CopyDeep());
        }
    }
}
