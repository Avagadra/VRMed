using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResultService
{
    DB db;
    public TestResultService()
    {
        db = new DB();
    }

    public void CreateResultTable()
    {
        db.GetConnection().DropTable<BDTestResult>();
        db.GetConnection().CreateTable<BDTestResult>();
    }

    public void CreateSimulatorResultTable()
    {
        db.GetConnection().DropTable<BDSimulatorResult>();
        db.GetConnection().CreateTable<BDSimulatorResult>();
    }

    public int AddResult(BDTestResult result)
    {
        return db.GetConnection().Insert(result);
    }

    public IEnumerable<BDTestResult> GetResult()
    {
        return db.GetConnection().Table<BDTestResult>();
    }
    public IEnumerable<BDSimulatorResult> GetResultPractice()
    {
        return db.GetConnection().Table<BDSimulatorResult>();
    }

    public IEnumerable<BDTestResult> GetResultForIDStudent(int id)
    {
        return db.GetConnection().Table<BDTestResult>().Where(x => x.IDStudent == id);
    }

    public IEnumerable<BDSimulatorResult> GetResultPracticeForIDStudent(int id)
    {
        return db.GetConnection().Table<BDSimulatorResult>().Where(x => x.IDStudent == id);
    }

    public int AddSimulatorResult(BDSimulatorResult result)
    {
        return db.GetConnection().Insert(result);
    }

    public IEnumerable<BDSimulatorResult> GetSimulatorResult()
    {
        return db.GetConnection().Table<BDSimulatorResult>();
    }

    public IEnumerable<BDSimulatorResult> GetSimulatorResultForIDStudent(int id)
    {
        return db.GetConnection().Table<BDSimulatorResult>().Where(x => x.IDStudent == id);
    }

    public BDSimulatorResult GetSimulatorResultForDate(string date)
    {
        return db.GetConnection().Table<BDSimulatorResult>().Where(x => x.DatePassage == date).FirstOrDefault();
    }

    public int UpdateSimulatorResult(BDSimulatorResult result)
    {
        return db.GetConnection().Update(result);
    }
}