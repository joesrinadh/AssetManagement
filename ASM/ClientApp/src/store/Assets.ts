import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { IAsset } from '../common/Asset'
// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface AssetsState {
    isLoading: boolean;
    assets: IAsset[];
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestAssetsAction {
    type: 'REQUEST_ASSETS';
}

interface ReceiveAssetsAction {
    type: 'RECEIVE_ASSETS';
    assets: IAsset[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestAssetsAction | ReceiveAssetsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestAssets: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.assets) {
            fetch(`api/Asset/GetAllAssets`)
                .then(response => response.json() as Promise<IAsset[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_ASSETS', assets: data });
                });

            dispatch({ type: 'REQUEST_ASSETS' });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: AssetsState = { assets: [], isLoading: false };

export const reducer: Reducer<AssetsState> = (state: AssetsState | undefined, incomingAction: Action): AssetsState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_ASSETS':
            return {
                assets: state.assets,
                isLoading: true
            };
            break;
        case 'RECEIVE_ASSETS':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            return {
                assets: action.assets,
                isLoading: false
            };
            break;
    }

    return state;
};
