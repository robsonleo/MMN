using System;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GUI.ViewModel
{
    public class FormatValidator : ValidatorBase
    {
        private class FuncState : ValidatorStateBase
        {
            public FuncState(ValidatorBase context) : base(context)
            {
            }

            public override void Validate()
            {
                Func<bool> predicate = ((FormatValidator)Context)._predicate;

                if (predicate.Invoke())
                {
                    Context.State = Context.CustomState;
                    Context.Validate();
                }
            }
        }

        private readonly Func<bool> _predicate;
        /// <summary>
        /// Конструктор
        /// </summary>
        public FormatValidator(FieldViewModelBase field, Func<bool> predicate) : base(field)
        {
            _predicate = predicate;

            StartState = new FuncState(this);
            State = StartState;
        }
    }
}
