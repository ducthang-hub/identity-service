namespace IdentityServer
{
    public static class Common
    {
        public static int AccessTokenLifeTime = 60 * 60 * 4; // seconds
        public static int RefreshTokenLifeTime = 60 * 60 * 24 * 4; // seconds

        public const string StudentApiResource = "StudentApiResource";
        public const string TeacherApiResource = "TeacherApiResource";
        public const string AdminApiResource = "AdminApiResource";

        public const string StudentScope = "student_scope";
        public const string TeacherScope = "teacher_scope";
        public const string AdminScope = "admin_scope";

        public const string EwbStudentWebClient = "ewb-student-web";
        public const string EwbStudentMobileClient = "ewb-student-mobile";
        public const string EwbTeacherClient = "ewb-teacher";
        public const string EwbAdminClient = "ewb-admin";
    }
}
