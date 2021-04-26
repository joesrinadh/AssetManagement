import * as Assets from './Assets';
import * as Asset from './Asset';

// The top-level state object
export interface ApplicationState {
    assets: Assets.AssetsState | undefined;
    asset: Asset.AssetState | undefined;
}

// Reducers
export const reducers = {
    assets: Assets.reducer,
    asset: Asset.reducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
