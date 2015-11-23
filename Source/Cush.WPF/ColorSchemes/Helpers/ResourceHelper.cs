using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes.Helpers
{
    internal class ResourceHelper
    {
        internal void AddOrReplaceResources(ResourceDictionary newRd, ResourceDictionary oldRd)
        {
            oldRd.BeginInit();

            foreach (DictionaryEntry r in newRd)
            {
                if (oldRd.Contains(r.Key))
                    oldRd.Remove(r.Key);

                oldRd.Add(r.Key, r.Value);
            }

            oldRd.EndInit();
        }

        internal List<ResourceContainer> PopulateResourceContainers(
            IEnumerable<IKeyedResourceContainer> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            var resourceExtensions = dictionary as IList<IKeyedResourceContainer> ?? dictionary.ToList();

            return resourceExtensions.Select(item => new ResourceContainer(item.DisplayName, item.Resources.Source.AbsoluteUri)).ToList();
        }

        internal void RemoveDictionary(ResourceDictionary dictionaryToRemove, ResourceDictionary parent)
        {
            var oldDictionary = parent.MergedDictionaries.FirstOrDefault(d => d.Source == dictionaryToRemove.Source);
            if (oldDictionary != null)
            {
                parent.MergedDictionaries.Remove(oldDictionary);
            }
        }

        internal void AddDictionary(ResourceDictionary dictionaryToAdd, ResourceDictionary parent)
        {
            parent.MergedDictionaries.Add(dictionaryToAdd);
        }

        internal void AddDictionaryIfMissing(ResourceDictionary dictionaryToAdd, ResourceDictionary parent)
        {
            if (parent.MergedDictionaries.Contains(dictionaryToAdd)) return;
            AddDictionary(dictionaryToAdd, parent);
        }

    }
}