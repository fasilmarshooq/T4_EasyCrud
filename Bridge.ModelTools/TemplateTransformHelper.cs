using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Bridge.ModelTools.Templates;

namespace Bridge.ModelTools
{
    public class TemplateTransformHelper
    {
        public static bool IsGeneratingCode { get; private set; }
        public static void Transform(string SourcePath)
        {
            if (IsGeneratingCode) throw new Exception("Code generation is already in progress. Please retry later.");

            try
            {
                MetaModel metaModel = MetaModel.GenerateMetaModel(SourcePath);

                var entities = new EntityTemplate().TransformText();

                var entityTypeTargetPath = Path.Combine(SourcePath, "Entities.cs");

                File.WriteAllText(entityTypeTargetPath, entities);


                var repository = new RepositoryTemplate().TransformText();
                var repositoryTargetPath = Path.Combine(SourcePath, "Repository.cs");
                File.WriteAllText(repositoryTargetPath, repository);
            }
            catch (Exception)
            {

                throw;
            }
          
        }
    }
}
