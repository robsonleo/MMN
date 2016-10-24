using System;

namespace GUI
{
    /// <summary>
    /// Интерфейс для обьекта-сериализатора
    /// </summary>
    public interface IKtcSerializer
    {
        /// <summary>
        /// Сериализация справочника
        /// </summary>
        /// <param name="dicionary"> справочник</param>
        /// <param name="name">название справочника</param>
        /// <returns> результат сериализации</returns>
        bool SerializeDictionary(object dicionary, string name);
        /// <summary>
        /// Десериализация справочника
        /// </summary>
        /// <param name="name"> название</param>
        /// <returns> упакованый обьект справочника </returns>
        object DeserializeDictionary(string name);
        /// <summary>
        /// Сериализация документа
        /// </summary>
        /// <param name="document"> обьект документа</param>
        /// <param name="name"> название документа</param>
        /// <returns> результат сериализации</returns>
        bool Serialize(object document, string name);
        /// <summary>
        /// Десериализация документа
        /// </summary>
        /// <param name="type"> тип документа</param>
        /// <param name="name"> название документа</param>
        /// <returns> упакованый обьект справочника</returns>
        object Deserialize(string name, Type type);
    }
}
