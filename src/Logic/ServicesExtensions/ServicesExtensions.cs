using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ServicesExtensions
{
    public static class ServicesExtensions
    {

        public static void AddServices(this IServiceCollection services, string studentDB, string baseDB)
        {
            services.AddSingleton(typeof(Interfaces.ISessionCacheProvider), new SessionCache.SessionCacheProvider());
            services.AddScoped<Interfaces.ISessionBL, Business.SessionBL>();
            services.AddScoped<Interfaces.IQuizProvider, Providers.QuizProvider>();

            services.AddSingleton<DB.StudentDBConnectionUtility>(new DB.StudentDBConnectionUtility(studentDB));
            services.AddSingleton<DB.BaseDBConnectionUtility>(new DB.BaseDBConnectionUtility(baseDB));
        }

        /*public static void AddServices(this IServiceCollection services)
        {
            List<string> allFilesToProcess = new List<string>();

            string binLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string[] files = System.IO.Directory.GetFiles(binLocation, "*.bin");

            if ( files != null)
            {
                allFilesToProcess.AddRange(files);
            }

            files = System.IO.Directory.GetFiles(binLocation, "*.dll");


            if (files != null)
            {
                allFilesToProcess.AddRange(files);
            }

            Dictionary<Type, List<Type>> classesByTypes = new Dictionary<Type, List<Type>>();
            HashSet<Type> classesTypes = new HashSet<Type>();;

            foreach (var file in allFilesToProcess)
            {
                try
                {
                    var assembly = System.Reflection.Assembly.LoadFile(file);

                    if (assembly != null)
                    {
                        Type[] typesToEvaluate = assembly.GetExportedTypes();

                        foreach(var t in typesToEvaluate)
                        {
                            evaluateType(t, classesByTypes, classesTypes);
                        }
                    }
                }
                catch
                {
                    
                }
            }

            foreach(var pair in classesByTypes)
            {
                services.AddTransient<>
            }

        }

        private static void evaluateType(Type t, Dictionary<Type, List<Type>> classesByTypes, HashSet<Type> classesTypes)
        {
            if (canEvaluate(t, classesByTypes, classesTypes))
            {
                if (t.IsInterface)
                {
                    List<Type> classes;
                    if (!classesByTypes.TryGetValue(t, out classes))
                    {
                        classes = new List<Type>();
                        classesByTypes.Add(t, classes);
                    }
                }
                else
                {
                    foreach (var interfaceType in t.GetInterfaces())
                    {
                        evaluateType(interfaceType, classesByTypes, classesTypes);

                        List<Type> classes;
                        if (classesByTypes.TryGetValue(interfaceType, out classes))
                        {
                            classes.Add(t);
                        }
                        else
                        {
                            classesTypes.Add(t);
                        }
                    }
                }
            }
        }

        static bool canEvaluate(Type t, Dictionary<Type, List<Type>> classesByTypes, HashSet<Type> classesTypes)
        {
            if (t == null || !t.IsPublic)
            {
                return false;
            }

            if (classesByTypes.ContainsKey(t))
                return false;

            if (classesTypes.Contains(t))
                return false;

            if (!t.IsClass && !t.IsInterface)
            {
                return false;
            }

            var customAttributes = t.GetCustomAttributes(typeof(Logic.ServicesExtensions.ServiceAttribute), false);

            if (customAttributes != null)
            {
                return true;
            }

            return false;
        }*/
    }
}
