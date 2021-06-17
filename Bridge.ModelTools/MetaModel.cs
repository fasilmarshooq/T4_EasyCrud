using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bridge.ModelTools
{

    public class MetaModel
    {
        private string _currentXMLFilePaths;

        public ICollection<Entity> EntityModel { get; set; }

        public static MetaModel metaModel = null;

        private MetaModel(string currentXMLfilePaths)
        {
            _currentXMLFilePaths = currentXMLfilePaths;
            LoadModel();
        }

        public static MetaModel GenerateMetaModel(string currentXMLfilePaths)
        {
            try
            {
                if (metaModel == null)
                {
                    metaModel = new MetaModel(currentXMLfilePaths);
                }
                return metaModel;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static MetaModel TryGetMetaModel()
        {
            if (metaModel != null)
            {
                return metaModel;
            }
            else
            {
                throw new Exception("Model cotext files not set and model is not loaded.");
            }
        }

        private void LoadModel()
        {
            string entities = Path.Combine(_currentXMLFilePaths, Messages.EntityTypesFileName);
            IXmlLoader xml = new EntityXmlLoader();
            this.EntityModel = xml.Load(entities);
        }

       
      
    }
}
