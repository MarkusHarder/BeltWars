public interface IEntity
{
    void Initialize(); //Function without any arguments
    float health { get; set; } //A variable
    void ApplyDamage(float points); //Function with one argument
}