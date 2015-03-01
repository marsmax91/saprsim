﻿using sapr_sim.WPFCustomElements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace sapr_sim.Utils
{
    public class FileService
    {

        public static readonly string PROJECT_EXTENSION = ".ssp";
        public static readonly string PROJECT_ITEM_EXTENSION = ".ssm";

        public ScrollableCanvas open(string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                return (ScrollableCanvas) new BinaryFormatter().Deserialize(fs);
            }
        }

        public void save(Canvas currentCanvas, string filepath)
        {
            using (FileStream filestream = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                new BinaryFormatter().Serialize(filestream, currentCanvas);
            }
        }

        public void saveProject()
        {
            Project prj = Project.Instance;
            string pathToProject = prj.FullPath;
            if (!Directory.Exists(pathToProject))
                Directory.CreateDirectory(pathToProject);

            string projectFile = pathToProject + "\\" + prj.ProjectName + PROJECT_EXTENSION;
            XmlSerializer serializer = new XmlSerializer(typeof(Project));
            using (var writer = new StreamWriter(projectFile))
            {
                serializer.Serialize(writer, Project.Instance);
            }
        }
    }
}
