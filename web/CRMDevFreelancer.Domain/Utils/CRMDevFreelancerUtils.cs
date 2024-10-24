namespace CRMDevFreelancer;

public class CRMDevFreelancerUtils
{

    public static bool IsDebugMode()
    {
#if DEBUG
        return true;
#endif

        return false;
    }

}
