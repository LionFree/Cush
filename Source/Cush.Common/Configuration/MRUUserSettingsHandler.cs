using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Cush.Common.FileHandling;
using Cush.Common.Logging;
using JetBrains.Annotations;
// ReSharper disable UnusedMember.Global

namespace Cush.Common.Configuration
{
    [CLSCompliant(true)]
    [DebuggerDisplay("Count: {_userSettings?.MRUEntries?.Count ?? 0}")]
    public class MRUUserSettingsHandler
    {
        private readonly ILogger _logger;
        private readonly MRUUserSettings _userSettings;

        public MRUUserSettingsHandler(ILogger logger)
        {
            _userSettings = new MRUUserSettings();
            _logger = logger;
        }

        public ObservableCollection<MRUEntry> Read()
        {
            try
            {
                var entries = _userSettings.MRUEntries;
                return GetObservableCollection(entries);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }
        
        public void Save([NotNull] IEnumerable<MRUEntry> entries)
        {
            try
            {
                _userSettings.MRUEntries = GetMRUList(entries);
                Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        public void Save()
        {
            try
            {
                _userSettings.Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        public void Add([NotNull] MRUEntry entry)
        {
            _userSettings.MRUEntries.Add(entry);
        }

        public void Remove([NotNull] MRUEntry entry)
        {
            _userSettings.MRUEntries.Remove(entry);
        }

        public void Clear()
        {
            _userSettings.MRUEntries.Clear();
        }
        
        private MRUList GetMRUList([NotNull] IEnumerable<MRUEntry> entries)
        {
            var al = new MRUList();
            foreach (var entry in entries)
            {
                al.Add(entry);
            }
            return al;
        }

        private ObservableCollection<MRUEntry> GetObservableCollection([NotNull] MRUList list)
        {
            var output = new ObservableCollection<MRUEntry>();
            foreach (DictionaryEntry entry in list)
            {
                output.Add((MRUEntry)entry.Value);
            }
            return output;
        }

    }
}