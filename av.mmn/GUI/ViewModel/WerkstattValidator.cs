using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    class WerkstattValidator : ValidatorBase
    {
        private class WerkstattValidateState : ValidatorStateBase
        {
            //public WerkstattFlags Flags { get; }

            public WerkstattValidateState(ValidatorBase context/*, WerkstattFlags flags*/) : base(context)
            {
                //Flags = flags;
            }

            public override void Validate()
            {
                FieldViewModelBase field = Context.Field;

                foreach (var i in field.Value.Split('/').SelectMany(value => value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(p => short.Parse(p.Split('=')[0]))))
                {
                    //todo uncoment if (KtcCache.Werkstatts.ContainsKey(i))
                    //{
                    //    //якщо цех позначений необхідним флагом
                    //    if ((KtcCache.Werkstatts[i].Flags & Flags) != 0)
                    //        continue;

                    //    field.IsValid = false;
                    //    field.ErrorInfo =
                    //        new ErrorInfoViewModel(ErrorType.FIELD_DIRECTORY_NOT_FOUND)
                    //        {
                    //            ErrorInfo = $@"Цех {i} не обозначен в справочнике как: {KtcCache.WerkstattErrorTypes[Flags]}"
                    //        };
                    //    return;
                    //}

                    field.IsValid = false;
                    field.ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_DIRECTORY_NOT_FOUND, "Цех " + i + " не найден в справочнике цехов\n");
                    return;
                }

                Context.State = Context.CustomState;
                Context.Validate();
            }
        }

        public WerkstattValidator(FieldViewModelBase field, string regex/* todo uncoment , WerkstattFlags flags*/) : base(field)
        {
            //todo uncoment StartState = new RegexValidatorState(this, regex, new WerkstattValidateState(this, flags));
            //State = StartState;

        }
    }
}
