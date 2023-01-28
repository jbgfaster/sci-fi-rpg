using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStateID//перечисления стэйтов для АИ
{
    Chase,
    Attack  
}

public interface AIState//интерфейс для реализации в каждом стэйте трёх методов. Требуется наследование этого интерфейса при создании нового стэйта
{
    AIStateID GetID();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
    
}
