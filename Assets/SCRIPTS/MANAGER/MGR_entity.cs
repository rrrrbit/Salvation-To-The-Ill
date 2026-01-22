using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MGR_entity : MonoBehaviour
{
    public GameObject pickupPrefab;
    public GameObject npcPrefab;

    public RandomEntitySettings spawnSettings;
    public RandomPickupSettings pickupSpawnSettings;

    public List<ENTITY> entities = new();
    public float recalcPathsTime = .1f;
    float recalcPathsTimer = 0;
    public LayerMask pickupLayer;
    public LayerMask entityLayers;
    public Transform itemParents;

    // Update is called once per frame
    void Update()
    {
        recalcPathsTimer -= Time.deltaTime;
        if(recalcPathsTimer < 0)
        {
            recalcPathsTimer = recalcPathsTime;
            foreach(NPC npc in entities.Where(x => x.GetType() == typeof(NPC)))
            {
                ((NPC_movement)npc.movement).RecalcPath();
            }
        }
    }

    public void createRandomNpc()
    {
        var thisNpc = RandomNPC();
        thisNpc.transform.position = new(0,3,0);
    }

    public GameObject RandomPickup()
    {
        var thisPickup = Instantiate(pickupPrefab);
        var p = thisPickup.GetComponent <OBJ_pickup>();

        var item = pickupSpawnSettings.items[RandomIndex(pickupSpawnSettings.itemChances.Select(x => x.Evaluate(MGR.game.difficulty)).ToList())];
        var thisItem = Instantiate(item, MGR.entities.itemParents);
        p.item = thisItem;

        return thisPickup;
    }

    public GameObject RandomNPC()
    {
        var thisNpc = Instantiate(npcPrefab);
        var chanceOfArmed = MGR.game.difficulty * CountTeam(ENTITY.Teams.HUMAN) / 
            (Mathf.Sqrt(CountTeam(ENTITY.Teams.ZOMBIE) + 1) + MGR.game.difficulty * CountTeam(ENTITY.Teams.HUMAN));

        print(chanceOfArmed);
        var health = RandAttrOverDiff(spawnSettings.minHealth, spawnSettings.maxHealth, spawnSettings.healthSkew);
        var size = RandAttrOverDiff(spawnSettings.minSize, spawnSettings.maxSize, spawnSettings.sizeSkew);
        var speed = RandAttrOverDiff(spawnSettings.minSpeed, spawnSettings.maxSpeed, spawnSettings.speedSkew);
        var defense = RandAttrOverDiff(spawnSettings.minDefense, spawnSettings.maxDefense, spawnSettings.defenseSkew);
        var convResistance = RandAttrOverDiff(spawnSettings.minConvResistance, spawnSettings.maxConvResistance, spawnSettings.convResistanceSkew);

        var item = spawnSettings.items[RandomIndex(spawnSettings.itemChances.Select(x => x.Evaluate(MGR.game.difficulty)).ToList())];
        var quality = (ItemData.Qualities)RandomIndex(spawnSettings.qualityChances.Select(x => x.Evaluate(MGR.game.difficulty)).ToList());

        var stats = thisNpc.GetComponent<Stats>();
        stats.maxHealth = health;
        stats.health = health;
        stats.defense = defense;
        stats.convResistance = convResistance;
        stats.speed = speed;
        stats.size = size;
        var inv = thisNpc.GetComponent<Inventory>();
        inv.InitInventory();
        if(Random.value <= chanceOfArmed)
        {
            var thisItem = Instantiate(item, MGR.entities.itemParents);
            inv.inventory[0] = thisItem.GetComponent<ItemData>();
            inv.inventory[0].quality = quality;
        }

        return thisNpc;
    }

    float RandAttrOverDiff(AnimationCurve min, AnimationCurve max, AnimationCurve curve) => Mathf.Lerp(min.Evaluate(MGR.game.difficulty), max.Evaluate(MGR.game.difficulty), curve.Evaluate(Random.value));
    int CountTeam(ENTITY.Teams team) => entities.Where(x => x.team == team).Count();
    int RandomIndex(List<float> chances)
    {
        var val = Random.Range(0, chances.Sum());
        var place = 0f;
        var i = 0;
        for (i = 0; i < chances.Count(); i++)
        {
            place += chances[i];
            if (place >= val) break;
        }
        return i;
    }
}

[CustomEditor(typeof(MGR_entity))]
public class FAWK : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MGR_entity myScript = (MGR_entity)target;
        if (GUILayout.Button("Build Object"))
        {
            myScript.createRandomNpc();
        }
    }
}