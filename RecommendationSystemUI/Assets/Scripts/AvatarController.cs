using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public void UploadNewAvatar() => MenuInteractions.Current.AccountController.UploadPhoto();
}
