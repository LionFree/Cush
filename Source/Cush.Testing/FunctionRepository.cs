using System;
using System.Collections.Generic;
using System.Linq;

namespace Cush.Testing
{
    public abstract class FunctionRepository
    {
        public static FunctionRepository GetInstance()
        {
            return new FunctionRepositoryImplementation();
        }

        public abstract void AddMethod<TResult>(Func<TResult> del);
        public abstract void AddMethod<T1, TResult>(Func<T1, TResult> del);
        public abstract void AddMethod<T1, T2, TResult>(Func<T1, T2, TResult> del);
        public abstract void AddMethod<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> del);
        public abstract void AddMethod<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> del);
        public abstract void AddMethod<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> del);
        public abstract object GetValue<TResult>(params object[] args);

        private class FunctionRepositoryImplementation : FunctionRepository
        {
            private readonly List<TypeObject> _list;

            internal FunctionRepositoryImplementation()
            {
                _list = new List<TypeObject>();
            }

            public override object GetValue<TResult>(params object[] args)
            {
                var metadata = GetMetadata<TResult>(args);
                return InvokeFunction<TResult>(metadata, args);
            }

            private TResult InvokeFunction<TResult>(TypeObject type, params object[] args)
            {
                var func = type.Function;
                var output = func.DynamicInvoke(args);
                return (TResult) output;
            }

            #region AddMethod

            public override void AddMethod<TResult>(Func<TResult> del)
            {
                var metadata = new TypeObject(typeof (TResult), del, null);
                _list.Add(metadata);
            }

            public override void AddMethod<T1, TResult>(Func<T1, TResult> del)
            {
                var metadata = new TypeObject(typeof (TResult), del, typeof (T1));
                _list.Add(metadata);
            }

            public override void AddMethod<T1, T2, TResult>(Func<T1, T2, TResult> del)
            {
                var metadata = new TypeObject(typeof (TResult), del, typeof (T1), typeof (T2));
                _list.Add(metadata);
            }

            public override void AddMethod<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> del)
            {
                var metadata = new TypeObject(typeof (TResult), del, typeof (T1), typeof (T2), typeof (T3));
                _list.Add(metadata);
            }

            public override void AddMethod<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> del)
            {
                var metadata = new TypeObject(typeof (TResult), del, typeof (T1), typeof (T2), typeof (T3), typeof (T4));
                _list.Add(metadata);
            }

            public override void AddMethod<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> del)
            {
                var metadata = new TypeObject(typeof (TResult), del, typeof (T1), typeof (T2), typeof (T3), typeof (T4),
                    typeof (T5));
                _list.Add(metadata);
            }

            #endregion

            #region Validation

            private void ValidateGenericType<T>(params object[] args)
            {
                if (!DictionaryContainsMethod<T>(args))
                    ThrowException<T>(args);
            }

            private bool DictionaryContainsMethod<T>(params object[] args)
            {
                return _list.Any(item => MetadataCorrespondingToTypeAndArgs<T>(item, args) != null);
            }

            private TypeObject GetMetadata<TResult>(params object[] args)
            {
                ValidateGenericType<TResult>(args);
                return
                    _list.Select(item => MetadataCorrespondingToTypeAndArgs<TResult>(item, args))
                        .FirstOrDefault(key => key != null);
            }

            private TypeObject MetadataCorrespondingToTypeAndArgs<T>(TypeObject item, params object[] args)
            {
                if (item.ReturnType != typeof (T)) return null;
                if (item.ArgsCount != args.Length) return null;

                var foundIt = !args.Where((t, i) => item.ArgTypes[i] != t.GetType()).Any();

                return foundIt ? item : null;
            }

            private void ThrowException<T>(params object[] args)
            {
                string argsMessage;
                if (args.Length == 0)
                {
                    argsMessage = "no arguments";
                }
                else
                {
                    argsMessage = "arguments (";
                    for (var i = 0; i < args.Count(); i++)
                    {
                        if (argsMessage != "arguments (")
                        {
                            argsMessage += ", ";
                        }

                        argsMessage += args[i].GetType();
                    }
                    argsMessage += ")";
                }

                var exceptionMessage =
                    string.Format("Cannot find appropriate method for type {0} with {1}.", typeof (T),
                        argsMessage);

                throw new ArgumentException(exceptionMessage);
            }

            #endregion
        }

        private class TypeObject
        {
            private readonly Type[] _argTypes;
            private readonly Delegate _function;
            private readonly Type _type;

            internal TypeObject(Type tResult, Delegate function, params Type[] tArgs)
            {
                _type = tResult;
                _argTypes = tArgs;
                _function = function;
            }

            internal Type ReturnType
            {
                get { return _type; }
            }

            internal int ArgsCount
            {
                get { return _argTypes == null ? 0 : _argTypes.Length; }
            }

            internal Type[] ArgTypes
            {
                get { return _argTypes; }
            }

            internal Delegate Function
            {
                get { return _function; }
            }
        }
    }
}