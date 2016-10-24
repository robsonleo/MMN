using System.Collections.Generic;
using System.Windows;
using System.Windows.Interactivity;
using TriggerBase = System.Windows.Interactivity.TriggerBase;

namespace GUI
{
    /// <summary>
    /// Прикрипленные поведения
    /// </summary>
    public class Behaviors : List<Behavior>
    {
    }
    /// <summary>
    /// Прикрепленные триггеры
    /// </summary>
    public class Triggers : List<TriggerBase>
    {
    }
    /// <summary>
    /// Интерактивные  
    /// </summary>
    public static class SupplementaryInteraction
    {
        /// <summary>
        /// Так исторически сложилось
        /// </summary>
        public static Behaviors GetBehaviors(DependencyObject obj)
        {
            return (Behaviors)obj.GetValue(BehaviorsProperty);
        }
        /// <summary>
        /// Так исторически сложилось
        /// </summary>
        public static void SetBehaviors(DependencyObject obj, Behaviors value)
        {
            obj.SetValue(BehaviorsProperty, value);
        }
        /// <summary>
        /// Так исторически сложилось
        /// </summary>
        public static readonly DependencyProperty BehaviorsProperty =
            DependencyProperty.RegisterAttached("Behaviors", typeof(Behaviors), typeof(SupplementaryInteraction), new UIPropertyMetadata(null, OnPropertyBehaviorsChanged));

        private static void OnPropertyBehaviorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behaviors = Interaction.GetBehaviors(d);
            if (e.NewValue != null)
                foreach (var behavior in (Behaviors)e.NewValue)
                    behaviors.Add(behavior);
        }
        /// <summary>
        /// Так исторически сложилось
        /// </summary>
        public static Triggers GetTriggers(DependencyObject obj)
        {
            return (Triggers)obj.GetValue(TriggersProperty);
        }
        /// <summary>
        /// Так исторически сложилось
        /// </summary>
        public static void SetTriggers(DependencyObject obj, Triggers value)
        {
            obj.SetValue(TriggersProperty, value);
        }
        /// <summary>
        /// Так исторически сложилось
        /// </summary>
        public static readonly DependencyProperty TriggersProperty =
            DependencyProperty.RegisterAttached("Triggers", typeof(Triggers), typeof(SupplementaryInteraction), new UIPropertyMetadata(null, OnPropertyTriggersChanged));

        private static void OnPropertyTriggersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var triggers = Interaction.GetTriggers(d);
            if (e.NewValue != null)
                foreach (var trigger in (Triggers)e.NewValue)
                    triggers.Add(trigger);
        }
    }
}
