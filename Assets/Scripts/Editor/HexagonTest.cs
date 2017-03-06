using Hexes;
using NUnit.Framework;
using UnityEngine;

public class HexagonTest {

	[Test]
	public void ConstructHexagon() {
        //Arrange
        Hexagon hex = new Hexagon(new Vector2(7, 9), false, 5);

		//Act

		//Assert
		Assert.AreEqual(new Vector2(7, 9), hex.position);
		Assert.AreEqual(false, hex.pointy);
		Assert.AreEqual(5, hex.size);
    }

    [Test]
    public void HexConors() {
        //Arrange
        Hexagon hexF = new Hexagon(new Vector2(-2, 3), false);
        Hexagon hexP = new Hexagon(new Vector2(-4, 8), true);

        //Act

        //Assert
        Assert.AreEqual(new Vector2(0.5F, Mathf.Sqrt(3) / 2), hexF.GetCornerLocalPosition(0));
        Assert.AreEqual(new Vector2(1, 0), hexF.GetCornerLocalPosition(1));
        Assert.AreEqual(new Vector2(0.5F, -Mathf.Sqrt(3) / 2), hexF.GetCornerLocalPosition(2));
        Assert.AreEqual(new Vector2(-0.5F, -Mathf.Sqrt(3) / 2), hexF.GetCornerLocalPosition(3));
        Assert.AreEqual(new Vector2(-1, 0), hexF.GetCornerLocalPosition(4));
        Assert.AreEqual(new Vector2(-0.5F, Mathf.Sqrt(3) / 2), hexF.GetCornerLocalPosition(5));

        Assert.AreEqual(new Vector2(0, 1), hexP.GetCornerLocalPosition(0));
        Assert.AreEqual(new Vector2(Mathf.Sqrt(3) / 2, 0.5F), hexP.GetCornerLocalPosition(1));
        Assert.AreEqual(new Vector2(Mathf.Sqrt(3) / 2, -0.5F), hexP.GetCornerLocalPosition(2));
        Assert.AreEqual(new Vector2(0, -1), hexP.GetCornerLocalPosition(3));
        Assert.AreEqual(new Vector2(-Mathf.Sqrt(3) / 2, -0.5F), hexP.GetCornerLocalPosition(4));
        Assert.AreEqual(new Vector2(-Mathf.Sqrt(3) / 2, 0.5F), hexP.GetCornerLocalPosition(5));

        //-2, 3
        Assert.AreEqual(new Vector2(-1.5F, 3+Mathf.Sqrt(3) / 2), hexF.GetCornerGlobalPosition(0));
        Assert.AreEqual(new Vector2(-1, 3), hexF.GetCornerGlobalPosition(1));
        Assert.AreEqual(new Vector2(-1.5F, 3-Mathf.Sqrt(3) / 2), hexF.GetCornerGlobalPosition(2));
        Assert.AreEqual(new Vector2(-2.5F, 3-Mathf.Sqrt(3) / 2), hexF.GetCornerGlobalPosition(3));
        Assert.AreEqual(new Vector2(-3, 3), hexF.GetCornerGlobalPosition(4));
        Assert.AreEqual(new Vector2(-2.5F, 3+Mathf.Sqrt(3) / 2), hexF.GetCornerGlobalPosition(5));
        //-4, 8
        Assert.AreEqual(new Vector2(-4, 9), hexP.GetCornerGlobalPosition(0));
        Assert.AreEqual(new Vector2(-4+Mathf.Sqrt(3) / 2, 8.5F), hexP.GetCornerGlobalPosition(1));
        Assert.AreEqual(new Vector2(-4+Mathf.Sqrt(3) / 2, 7.5F), hexP.GetCornerGlobalPosition(2));
        Assert.AreEqual(new Vector2(-4, 7), hexP.GetCornerGlobalPosition(3));
        Assert.AreEqual(new Vector2(-4-Mathf.Sqrt(3) / 2, 7.5F), hexP.GetCornerGlobalPosition(4));
        Assert.AreEqual(new Vector2(-4-Mathf.Sqrt(3) / 2, 8.5F), hexP.GetCornerGlobalPosition(5));
    }
}
