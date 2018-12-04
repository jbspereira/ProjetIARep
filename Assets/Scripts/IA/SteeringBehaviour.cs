using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour  {

    Vehicle vehicle;

    Vector2 m_vWanderTarget;

    private float m_dWanderRadius;
    private float m_dWanderDistance;
    private float m_dWanderJitter;
    private int m_iFlags = 0;
    private float m_deceleration;
    private float m_radiusPanic;
    private Transform m_target;
    private Vehicle m_pTargetAgent1;
    private Vector2 m_vOffset;

    private float m_radius;
    private float m_weightSeek;
    private float m_weightFlee;
    private float m_weightArrive;
    private float m_weightWander;
    private float m_weightSeparation;
    private float m_weightPursuit;
    private float m_weightOffsetPursuit;
    private float m_weightPlayerControlled;
    private float m_weightFleePanic;

    private enum behavior_type:int {
        none = 0x00000,
        seek = 0x00002,
        flee = 0x00004,
        arrive = 0x00008,
        wander = 0x00010,
        separation = 0x00020,
        pursuit = 0x00040,
        offset_pursuit = 0x00080,
        playerControlled=0x00100,
        fleepanic=0x00200
    };
    //private float maxForce;

    public SteeringBehaviour(Vehicle monVehicle,float radius,float distance,float jitter) {
        vehicle = monVehicle;

        m_dWanderRadius=radius;
        m_dWanderDistance=distance;
        m_dWanderJitter=jitter;

        float theta = Random.Range(0f, 1f) * Mathf.PI * 2;
        m_vWanderTarget = new Vector2(m_dWanderRadius*Mathf.Cos(theta), m_dWanderRadius/Mathf.Sin(theta));
        //maxForce = vehicle.sqrMaxForce;
    }

    public Vector2 Seek(Vector2 TargetPos) {
        Vector2 DesiredVelocity = (TargetPos - vehicle.getPosition()).normalized * vehicle.maxSpeed;
        Vector3 forceToApply = DesiredVelocity - vehicle.getVelocity();
        return forceToApply;
    }

    public Vector2 Flee(Vector2 TargetPos) {
        Vector2 DesiredVelocity = (vehicle.getPosition() - TargetPos).normalized * vehicle.maxSpeed;
        return (DesiredVelocity - vehicle.getVelocity());
    }

    public Vector2 FleePanicDistance(Vector2 TargetPos, float PanicDistanceSq) {
        if ((vehicle.getPosition() - TargetPos).sqrMagnitude > PanicDistanceSq) {
            return new Vector2(0, 0);
        }
        Vector2 DesiredVelocity = (vehicle.getPosition() - TargetPos).normalized * vehicle.maxSpeed;
        return (DesiredVelocity - vehicle.getVelocity());
    }

    public Vector2 Arrive(Vector2 TargetPos, float deceleration) {
        Vector2 ToTarget = TargetPos - vehicle.getPosition();
        //calculate the distance to the target position
        float dist = ToTarget.magnitude;
        if (dist > 0.01) {//le 0 ne marche pas bien ce qui paraît normal
            const float DecelerationTweaker = 0.3f;
            float speed = dist / (deceleration * DecelerationTweaker);
            speed = Mathf.Min(speed, vehicle.maxSpeed);

            Vector2 DesiredVelocity = ToTarget * speed /dist;
            return (DesiredVelocity - vehicle.getVelocity());
        }
        //on set la vitesse à zero pour qu'il arrête de bouger?
        vehicle.setVelocity(new Vector2(0, 0)); 
        //parait etre un peu de la triche mais la 
        // speed est tellement faible quand on arrive ici...
        return new Vector2(0, 0);
    }

    public Vector2 Wander() {
        //this behavior is dependent on the update rate, so this line must
        //be included when using time independent framerate.
        float JitterThisTimeSlice = m_dWanderJitter * Time.fixedDeltaTime;

        //first, add a small random vector to the target's position
        m_vWanderTarget += new Vector2(Random.Range(-1f, 1f) * JitterThisTimeSlice,
                                    Random.Range(-1f, 1f) * JitterThisTimeSlice);

        //reproject this new vector back on to a unit circle
        m_vWanderTarget.Normalize();

        //increase the length of the vector to the same as the radius
        //of the wander circle
        m_vWanderTarget *= m_dWanderRadius;

        //move the target into a position WanderDist in front of the agent
        Vector2 Target = m_vWanderTarget + m_dWanderDistance * vehicle.getVelocity().normalized;
        //Debug.DrawLine(vehicle.getPosition(), vehicle.getPosition() + Target, Color.blue);
        //and steer towards it
        return Target;
    }

    public Vector2 Pursuit(Vehicle evader) {
        //if the evader is ahead and facing the agent then we can just seek
        //for the evader's current position.
        Vector2 ToEvader = evader.getPosition() - vehicle.getPosition();
        float RelativeHeading = Vector2.Dot(vehicle.Heading(), evader.Heading());
        if ((Vector2.Dot(ToEvader, vehicle.Heading()) > 0) && (RelativeHeading < -0.95))
        //acos(0.95)=18 degs
        {
            return Seek(evader.getPosition());
        }
        //Not considered ahead so we predict where the evader will be.
        //the look-ahead time is proportional to the distance between the evader
        //and the pursuer; and is inversely proportional to the sum of the
        //agents' velocities
        float LookAheadTime = ToEvader.magnitude / (vehicle.maxSpeed + evader.getVelocity().magnitude);
        //now seek to the predicted future position of the evader
        return Seek(evader.getPosition() + evader.getVelocity() * LookAheadTime);
    }    public Vector2 offsetPursuit(Vehicle evader,Vector2 offset) {
        //if the evader is ahead and facing the agent then we can just seek
        //for the evader's current position.
        Vector2 offsetPos = evader.getPosition()+offset.y * evader.Heading()+offset.x*evader.Side();
        Vector2 ToEvader = offsetPos - vehicle.getPosition();
        //Not considered ahead so we predict where the evader will be.
        //the look-ahead time is proportional to the distance between the evader
        //and the pursuer; and is inversely proportional to the sum of the
        //agents' velocities
        float LookAheadTime = ToEvader.magnitude / (vehicle.maxSpeed + evader.getVelocity().magnitude);
        //now seek to the predicted future position of the evader
        return Arrive(offsetPos + evader.getVelocity() * LookAheadTime,m_deceleration);
    }

    public Vector2 Separation(float radius) {
        Vector2 SteeringForce=Vector2.zero;
        GameObject[] neighbors= GameObject.FindGameObjectsWithTag("vehicle");
        for (int a = 0; a < neighbors.Length; ++a) {
            Vector2 ToAgent = vehicle.getPosition() - neighbors[a].GetComponent<Rigidbody2D>().position;
            if ((neighbors[a] != vehicle.gameObject) && (m_pTargetAgent1!=null  && neighbors[a] != m_pTargetAgent1.gameObject) && ToAgent.magnitude < radius) {
                SteeringForce += ToAgent.normalized / ToAgent.magnitude;
            }
        }
        return SteeringForce;
    }

    public Vector2 PlayerControlled() {
        float moveH = Input.GetAxisRaw("Horizontal");
        float moveV = Input.GetAxisRaw("Vertical");
        if (moveH==0 && moveV == 0) {
            vehicle.setVelocity(Vector2.zero);
            return Vector2.zero;
        }
        Vector2 dir = new Vector2(moveH,moveV);
        return dir.normalized*vehicle.maxSpeed;
    }

    public Vector2 Calculate() {
        Vector2 resultanteForces = Vector2.zero;
        if (On(behavior_type.seek)) {
            resultanteForces += m_weightSeek*Seek(m_target.position);
        }
        if (On(behavior_type.flee)) {
            resultanteForces += m_weightFlee * Flee(m_target.position);
        }
        if (On(behavior_type.arrive)) {
            resultanteForces += m_weightArrive * Arrive(m_target.position,m_deceleration);
        }
        if (On(behavior_type.wander)) {
            resultanteForces += m_weightWander * Wander();
        }
        if (On(behavior_type.separation)) {
            resultanteForces += m_weightSeparation * Separation(m_radius);
        }
        if (On(behavior_type.pursuit)) {
            resultanteForces += m_weightPursuit * Pursuit(m_pTargetAgent1);
        }
        if (On(behavior_type.offset_pursuit)) {
            resultanteForces += m_weightOffsetPursuit * offsetPursuit(m_pTargetAgent1,m_vOffset);
        }
        if (On(behavior_type.playerControlled)) {
            resultanteForces += m_weightPlayerControlled* PlayerControlled();
        }
        if (On(behavior_type.fleepanic)) {
            resultanteForces += m_weightFleePanic* FleePanicDistance(m_target.position, m_radiusPanic);
        }
        return resultanteForces;
    }

    bool On(behavior_type type) { return (m_iFlags & ((int)type)) == (int)type; }

    public void FleeOn(Transform target, int weight=1) { m_iFlags |= (int)behavior_type.flee; m_weightFlee = weight; m_target = target; }
    public void SeekOn(Transform target, int weight = 1) { m_iFlags |= (int)behavior_type.seek; m_weightSeek = weight; m_target = target; }
    public void ArriveOn(float deceleration, Transform target,int weight = 1) {
        m_target = target;
        m_iFlags |= (int)behavior_type.arrive;
        m_deceleration = deceleration;
        m_weightArrive = weight;
    }
    public void WanderOn(int weight = 1) { m_iFlags |= (int)behavior_type.wander; m_weightWander = weight; }
    public void SeparationOn(float radius, int weight = 1) { m_iFlags |= (int)behavior_type.separation; m_weightSeparation = weight; m_radius = radius; }
    public void PursuitOn(Vehicle v1, int weight = 1) { m_iFlags |= (int)behavior_type.pursuit; m_weightPursuit = weight; m_pTargetAgent1 = v1; }
    public void OffsetPursuitOn(Vehicle v1, Vector2 offset, float deceleration, int weight = 1) {
        m_iFlags |= (int) behavior_type.offset_pursuit;
        m_vOffset = offset;
        m_pTargetAgent1 = v1;
        m_deceleration = deceleration;
        m_weightOffsetPursuit = weight;
        vehicle.SetColor(Color.blue);
    }
    public void PlayerOn(int weight = 1) {
        m_iFlags |= (int)behavior_type.playerControlled;
        m_weightPlayerControlled = weight;
        //vehicle.SetColor(Color.green);
    }

    public void fleePanicOn(float radius,Transform target,int weight = 1) {
        m_radiusPanic = radius;
        m_iFlags |= (int)behavior_type.fleepanic;
        m_target = target;
        m_weightFleePanic = weight;
        //vehicle.SetColor(Color.green);
    }

    public void resetWeight() {
        m_weightSeek=0;
        m_weightFlee=0;
        m_weightArrive=0;
        m_weightWander=0;
        m_weightSeparation=0;
        m_weightPursuit=0;
        m_weightOffsetPursuit=0;
        m_weightPlayerControlled=0;
        m_weightFleePanic=0;
    }
    public void reset() {
        m_iFlags = 0;
    }
}
