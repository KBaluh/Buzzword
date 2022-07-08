using System;

using MassTransit;

namespace Buzzword.Common.Extensions
{
    public static class GuidExtensions
    {
        /// <summary>
        /// Создаст новый <see cref="Guid"/> если текущий пустой
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static Guid CurrentOrNew(this Guid guid)
        {
            if (guid == Guid.Empty)
            {
                guid = NewId.NextGuid();
            }
            return guid;
        }

        /// <summary>
        /// Вернет текущий или входящий <see cref="Guid"/> если они не пустые, но если оба параметра пустые тогда сгенерирует новый <seealso cref="Guid.NewGuid"/>
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="inputGuid"></param>
        /// <returns></returns>
        public static Guid CurrentOrNew(this Guid guid, Guid inputGuid)
        {
            if (guid == Guid.Empty)
            {
                guid = inputGuid != Guid.Empty ? inputGuid : NewId.NextGuid();
            }
            return guid;
        }

        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static bool IsEmpty(this Guid? guid)
        {
            return guid == null || IsEmpty(guid.Value);
        }

        public static bool IsNotEmpty(this Guid guid)
        {
            return !IsEmpty(guid);
        }

        public static bool IsNotEmpty(this Guid? guid)
        {
            return !IsEmpty(guid);
        }
    }
}
