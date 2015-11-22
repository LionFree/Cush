using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Cush.Testing
{
    [DebuggerStepThrough]
    public static class NotifyPropertyChanged
    {
        public static NotifyExpectation<T> ShouldNotifyOn<T, TProperty>(this T owner,
            Expression<Func<T, TProperty>> propertyPicker)
            where T : INotifyPropertyChanged
        {
            return CreateExpectation(owner, propertyPicker, true);
        }

        public static NotifyExpectation<T> ShouldNotNotifyOn<T, TProperty>(this T owner,
            Expression<Func<T, TProperty>>
                propertyPicker)
            where T : INotifyPropertyChanged
        {
            return CreateExpectation(owner, propertyPicker, false);
        }

        public static NotifyExpectation<T> CreateExpectation<T, TProperty>(T owner,
            Expression<Func<T, TProperty>> pickProperty,
            bool eventExpected)
            where T : INotifyPropertyChanged
        {
            var propertyName = ((MemberExpression) pickProperty.Body).Member.Name;
            return new NotifyExpectation<T>(owner, propertyName, eventExpected);
        }
    }
}