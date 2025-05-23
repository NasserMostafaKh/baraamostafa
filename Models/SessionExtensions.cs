﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.Json;

namespace TequliasRestaurant.Models
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session,string key,T value) 
        {
            session.SetString(key,JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var josn=session.GetString(key);
            if(string.IsNullOrEmpty(josn))
            {
                return default(T);
            }
            else
            {
                return JsonSerializer.Deserialize<T>(josn);
            } 
        }
    }
}
