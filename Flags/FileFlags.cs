using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flags
{
    public class FileFlags
    {
        public const string FileExistsFailedFlag = "AppFlags_FileExistsFailedFlag";
        public const string DefaultUserImageFileName = "UserImage";

        public const short AllowOP = 1;
        public const short FileExistsTrue = 2;
        public const short FileExistsFalse = 3;
        public const short FileOPFailed = 4;
        public const short FileOPSucceed = 5;
    }
}
