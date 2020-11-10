﻿using System;
using System.IO;
using Newtonsoft.Json;
using Plugins.UnityMonstackCore.Loggers;
using Plugins.UnityMonstackCore.Utils;

namespace Plugins.UnityMonstackContentLoader.JSON
{
    public abstract class AbstractJSONContentSingleEntryRepository<T> : AbstractContentSignleEntryRepository<T>
    {
        protected AbstractJSONContentSingleEntryRepository(string filePath) : base(filePath)
        {
        }

        public override void Reload()
        {
            try
            {
                var dataAsByteArray = LocalStorageUtils.LoadBytesFromFile(FileSource, FilePath);
                var reader = new StreamReader(new MemoryStream(dataAsByteArray));
                var dataAsJson = reader.ReadToEnd();

                Entity = JsonConvert.DeserializeObject<T>(dataAsJson);
            }
            catch (Exception e)
            {
                UnityLogger.Error("Failed to load JSON file {} because {}", FilePath, e);
            }
        }
    }
}