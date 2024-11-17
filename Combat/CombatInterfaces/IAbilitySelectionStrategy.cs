public interface IAbilitySelectionStrategy // Requirements for a brain to correctly select an ability
{
    Random RandomNumber {get;set;}
    Ability SelectAbility(NPC self);
    Ability ChooseOffensiveAbility(Character self);
    Ability ChooseDefensiveAbility(Character self);
    Ability ChooseSupportiveAbility(Character self);
}