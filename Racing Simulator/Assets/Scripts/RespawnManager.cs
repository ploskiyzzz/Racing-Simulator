using UnityEngine;
using System.IO;
using System.Linq;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private KeyCode respawnKey = KeyCode.R;

    void Update()
    {
        if (Input.GetKeyDown(respawnKey))
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        string latestFile = GetLatestCheckpointFile();
        if (string.IsNullOrEmpty(latestFile))
        {
            Debug.LogWarning("��� ����������� ����������!");
            return;
        }

        // ��������� ������
        string json = File.ReadAllText(latestFile);
        CheckpointData data = JsonUtility.FromJson<CheckpointData>(json);

        Transform player = FindPlayer();
        if (player == null) return;

        // �������������
        player.position = data.Position;
        player.rotation = data.Rotation;

        // �������� ������
        if (player.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log("����� �������� �� ��������� ���������!");

        // ������� ��� ������ ����� (��������� ������ �������)
        DeleteOldCheckpoints(data.Timestamp);
    }

    private string GetLatestCheckpointFile()
    {
        string folder = Application.persistentDataPath + "/Checkpoints";
        if (!Directory.Exists(folder))
            return null;

        string[] files = Directory.GetFiles(folder, "cp_*.json");
        if (files.Length == 0)
            return null;

        // ��������� �� Timestamp � ����� ����� (��������� ����� �����)
        return files


.OrderByDescending(f => long.Parse(Path.GetFileNameWithoutExtension(f).Split('_')[1]))
            .FirstOrDefault();
    }

    private void DeleteOldCheckpoints(long keepTimestamp)
    {
        string folder = Application.persistentDataPath + "/Checkpoints";
        if (!Directory.Exists(folder)) return;

        string[] files = Directory.GetFiles(folder, "cp_*.json");
        foreach (string file in files)
        {
            long timestamp = long.Parse(Path.GetFileNameWithoutExtension(file).Split('_')[1]);
            if (timestamp != keepTimestamp)
            {
                File.Delete(file);
                Debug.Log($"������ ������ ��������: {file}");
            }
        }
    }

    private Transform FindPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        return player ? player.transform : null;
    }
}