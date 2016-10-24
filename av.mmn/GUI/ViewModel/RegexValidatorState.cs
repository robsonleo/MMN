using System;
using System.Text.RegularExpressions;

namespace GUI.ViewModel
{
    class RegexValidatorState : ValidatorStateBase
    {
        private readonly string _regex;

        private readonly ValidatorStateBase _nextState;

        public RegexValidatorState(ValidatorBase context, string regex, ValidatorStateBase nextState) : base(context)
        {
            if (context != nextState.Context)
                throw new InvalidOperationException("Неможливо використовувати стан від іншого контексту");

            _regex = regex;
            _nextState = nextState;
        }

        public override void Validate()
        {
            FieldViewModelBase field = Context.Field;

            if (string.IsNullOrEmpty(field.Value))
            {
                if (!field.AllowEmpty)
                {
                    field.IsValid = false;
                    field.ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY);
                    return;
                }
            }
            else if (Regex.Match(field.Value, _regex).Value != field.Value)
            {
                field.IsValid = false;
                field.ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
                return;
            }

            Context.State = _nextState;
            Context.Validate();
        }
    }
}
