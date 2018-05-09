using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {
    public static BattleController Instance;
    ICharacter[] monsters;
    Player player;
    ICharacter[] battleQueue;
    private void Awake()
    {
        Instance = this;
        player = PlayerControler.Instance.GetComponent<Player>();
    }

    public void StartBattle(RoomType type) {
        if (type == RoomType.Normal) {
            uint numOfMonsters;
            if (player.Score < 100) {
                numOfMonsters = 1;
            } else if (player.Score > 700) {
                numOfMonsters = 7;
            } else {
                numOfMonsters = player.Score/100;
            }
            monsters = new ICharacter[numOfMonsters];
            for (uint i = 0; i < numOfMonsters; i++) {
                AddMonster(i);
            }
            long queueSize = player.Monsters.Length + numOfMonsters + 1;
            battleQueue = new ICharacter[queueSize];
            for (uint i = 0; i < numOfMonsters; i++) {
                battleQueue[i] = monsters[i];
            }
            for (uint i = 0; i < player.Monsters.Length; i++) {
                battleQueue[i+numOfMonsters] = player.Monsters[i];
            }
            battleQueue[queueSize-1] = player;
            SortQueue();
            bool allDead = false;
            while (!allDead) {
                uint currentTurn = 0;
                while (currentTurn < queueSize) {
                    if (!battleQueue[currentTurn].IsDead) {
                        if (battleQueue[currentTurn].IsPlayer) {
                            // hurrru durrru menusy, walka i takie tam
                        } else {
                            MakeAMove(currentTurn);
                        }
                    }
                    currentTurn++;
                }
                currentTurn = 0;
                while ((battleQueue[currentTurn].IsDead || battleQueue[currentTurn].IsPlayer) && currentTurn < queueSize) {
                    currentTurn++;
                }
                if (currentTurn == queueSize) {
                    allDead = true;
                }
            }
        }
        
    }

    private void AddMonster(uint idx) {
        int rand = Random.Range(1,4);
        switch (rand) {
            case 1:
                monsters[idx] = new Dwarf();
                break;
            case 2:
                monsters[idx] = new Elf();
                break;
            case 3:
                monsters[idx] = new Orc();
                break;
            default:
                Debug.Log("ayy karramba, invalid monster id");
                AddMonster(idx);
                break;
        }
    }

    private void SortQueue() {
        for (uint i = 0; i < battleQueue.Length; i++) {
            for (uint j = 0; j < battleQueue.Length - i - 1; j++) {
                if (battleQueue[j].Speed <= battleQueue[j+1].Speed) {
                    ICharacter tmp = battleQueue[j];
                    battleQueue[j] = battleQueue[j+1];
                    battleQueue[j+1] = tmp;
                }
            }
        }
    }

    private void MakeAMove(uint idx) {
        
    }

    private void MeleeAttack(uint attacker, uint defender) {
        double maxAttackPower = battleQueue[attacker].Strength * 1.5 + battleQueue[attacker].Endurance * 0.5;
        double maxDefendPower = battleQueue[defender].Endurance;
        double attackPower = maxAttackPower * Random.Range(0f, 1f);
        double defendPower = maxDefendPower * Random.Range(0.5f, 1f);
        int damage = System.Convert.ToInt32(attackPower) - System.Convert.ToInt32(defendPower);
        if (damage < 0) {
            damage = 0;
        }
        battleQueue[defender].Damage(damage);
    }
}