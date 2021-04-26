import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { IAsset } from '../common/Asset'

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface AssetState {
    isLoading: boolean;
    assetId?: string;
    assets: IAsset[];
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestAssetAction {
    type: 'REQUEST_ASSET';
    assetId?: string;
}

interface ReceiveAssetAction {
    type: 'RECEIVE_ASSET';
    assetId?: string;
    assets: IAsset[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestAssetAction | ReceiveAssetAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestAssets: (assetId: string, isForceRefresh?: boolean): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if ((appState && appState.asset && assetId !== appState.asset.assetId) || isForceRefresh) {
            fetch(`api/Asset?Id=${assetId}`)
                .then(response => response.json() as Promise<IAsset[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_ASSET', assetId: assetId, assets: data });
                });

            dispatch({ type: 'REQUEST_ASSET', assetId: assetId });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: AssetState = { assets: [], isLoading: false };

export const reducer: Reducer<AssetState> = (state: AssetState | undefined, incomingAction: Action): AssetState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_ASSET':
            return {
                assetId: action.assetId,
                assets: state.assets,
                isLoading: true
            };
        case 'RECEIVE_ASSET':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (action.assetId === state.assetId) {
                return {
                    assetId: action.assetId,
                    assets: action.assets,
                    isLoading: false
                };
            }
            break;
    }

    return state;
};
