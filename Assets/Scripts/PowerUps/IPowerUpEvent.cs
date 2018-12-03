using UnityEngine.EventSystems;

public interface IPowerUpEvents : IEventSystemHandler {
    void OnPowerUpCollected(PowerUp powerUp);

    void OnPowerUpFinished(PowerUp powerUp);
}
