using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace OSMClient
{
    /// <summary>
    /// Provides access to content caches.
    /// Stores data in persistent data storage.
    /// </summary>
    public static class PersistentCache
    {
        public static bool Initialized = false;
        private static string persistentDataPath;
        public static string cacheDataPath;

        public static void Init()
        {
            Initialized = true;
            // 애플리케이션의 지속적인 데이터 저장 경로와 "PersistentCache" 폴더를 결합하여 캐시 데이터의 전체 경로를 생성
            persistentDataPath = Application.persistentDataPath;

            cacheDataPath = Path.Combine(persistentDataPath, "PersistentCache");

            //지정된 경로에 캐시 디렉토리가 존재하지 않으면 해당 디렉토리를 생성
            if (!Directory.Exists(cacheDataPath))
                Directory.CreateDirectory(cacheDataPath);
        }

        /// <summary>
        /// Clean cache directory
        /// </summary>
        public static void ClearPersistentStorage()
        {
            if (!Initialized)
                Init();

            if (Directory.Exists(cacheDataPath))
            {
                Directory.Delete(cacheDataPath, true);
                Directory.CreateDirectory(cacheDataPath);
            }
        }

        public static byte[] TryLoad(string key, TimeSpan outOfDatePeriod)
        {
            if (!Initialized)
                Init();

            var fullPath = GetPath(key);
            if (File.Exists(fullPath))
                try
                {
                    var lastWrite = File.GetLastWriteTime(fullPath);

                    if (DateTime.Now - lastWrite < outOfDatePeriod)
                    {
                        return File.ReadAllBytes(fullPath);
                    }
                    else
                    {
                        File.Delete(fullPath);
                    }
                }
                catch { }

            return null;
        }

        public static void TryLoadAsync(string key, TimeSpan outOfDatePeriod, ConcurrentQueue<(byte[] bytes, Action<byte[]> act)> queue, Action<byte[]> onCompleted)
        {
            if (!Initialized) //Initialized가 false인 경우 초기화
                Init();

            //ThreadPool.QueueUserWorkItem 사용해서 새로운 스레드에 데이터를 로드
            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bytes = TryLoad(key, outOfDatePeriod);
                if (onCompleted != null)
                    queue.Enqueue((bytes, onCompleted));
            });
        }

        public static string Save(string key, byte[] bytes)
        {
            if (!Initialized)
                Init();

            var fullPath = GetPath(key);
            File.WriteAllBytes(fullPath, bytes);

            return fullPath;
        }

        public static string SaveAsync(string key, byte[] bytes, Action onCompleted = null)
        {
            if (!Initialized)
                Init();

            var fullPath = GetPath(key);
            ThreadPool.QueueUserWorkItem((w) =>
            {
                Save(key, bytes);
                onCompleted?.Invoke();
            });

            return fullPath;
        }

        public static string GetPath(string key)
        {
            return Path.Combine(cacheDataPath, UnityWebRequest.EscapeURL(key));
        }

        public static bool Remove(string key)
        {
            var fullName = GetPath(key);
            if (File.Exists(fullName))
            {
                File.Delete(fullName);
                return true;
            }

            return false;
        }
    }
}