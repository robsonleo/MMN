

namespace GUI
{
    /// <summary>
    /// Сериализатор
    /// </summary>
    public static class KtcSerializer
    {
        /// <summary>
        /// Реализация сериализатора
        /// </summary>
        public static IKtcSerializer Serializer { get; }

        static KtcSerializer()
        {
            Serializer = Properties.ChecksSettings.Default.XML_Сериализация_Справочников
                ? (IKtcSerializer)new KtcXmlSerializer()
                : new KtcBinarySerializer();
        }
    }
}
